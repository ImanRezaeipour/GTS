<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HourlyRequestOnAbsence.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.HourlyRequestOnAbsence" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body onkeydown="HourlyRequestOnAbsenceForm_onKeyDown(event);">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="HourlyRequestOnAbsenceForm" runat="server" meta:resourcekey="HourlyRequestOnAbsenceForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td style="height: 10%">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbHourlyRequestOnAbsence" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbHourlyRequestOnAbsence" runat="server"
                                            ClientSideCommand="tlbItemDelete_TlbHourlyRequestOnAbsence_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbHourlyRequestOnAbsence"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbHourlyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbHourlyRequestOnAbsence"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbHourlyRequestOnAbsence_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbHourlyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbHourlyRequestOnAbsence_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbHourlyRequestOnAbsence" TextImageSpacing="5"
                                            Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbHourlyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbHourlyRequestOnAbsence_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbHourlyRequestOnAbsence"
                                            TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbHourlyRequestOnAbsence"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbHourlyRequestOnAbsence_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbHourlyRequestOnAbsence"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbHourlyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemExit_TlbHourlyRequestOnAbsence_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbHourlyRequestOnAbsence" TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="tdSelectedDate_HourlyRequestOnAbsence" class="HeaderLabel" style="width: 30%"></td>
                            <td runat="server" id="ActionMode_HourlyRequestOnAbsence" class="ToolbarMode" style="width: 10%"
                                meta:resourcekey="InverseAlignObj"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 25%">
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_AbsenceDetails_HourlyRequestOnAbsence" class="HeaderLabel" style="width: 50%">Absence Details
                                        </td>
                                        <td id="loadingPanel_GridAbsencePairs_RequestOnAbsence" class="HeaderLabel" style="width: 45%"></td>
                                        <td id="Td5" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridAbsencePairs_RequestOnAbsence" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridAbsencePairs_RequestOnAbsence"
                                                        runat="server" ClientSideCommand="Refresh_GridAbsencePairs_RequestOnAbsence();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridAbsencePairs_RequestOnAbsence"
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
                                <ComponentArt:CallBack ID="CallBack_GridAbsencePairs_RequestOnAbsence" runat="server"
                                    OnCallback="CallBack_GridAbsencePairs_RequestOnAbsence_onCallBack" Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridAbsencePairs_RequestOnAbsence" runat="server" AllowHorizontalScrolling="true"
                                            CssClass="Grid" EnableViewState="true" ShowFooter="false" FillContainer="true"
                                            FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                            PageSize="4" RunningMode="Client" Width="590" AllowMultipleSelect="false" AllowColumnResizing="false"
                                            ScrollBar="On" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                            ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                    DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                    HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                            HeadingText="نوع غیبت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnAbsenceType_GridAbsencePairs_RequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="From" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridAbsencePairs_RequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="To" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridAbsencePairs_RequestOnAbsence" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <Load EventHandler="GridAbsencePairs_RequestOnAbsence_onLoad" />
                                                <ItemSelect EventHandler="GridAbsencePairs_RequestOnAbsence_onItemSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_AbsencePairs" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridAbsencePairs_RequestOnAbsence_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridAbsencePairs_RequestOnAbsence_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" style="height: 15%">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 5%">
                                <input type="radio" runat="server" id="rdbLeaveRequest_HourlyRequestOnAbsence" name="HourlyRequestOnAbsence"
                                    onclick="rdbLeaveRequest_HourlyRequestOnAbsence_onClick();" />
                            </td>
                            <td style="width: 95%">
                                <asp:Label runat="server" ID="lblLeaveRequest_HourlyRequestOnAbsence" Text="درخواست مرخصی"
                                    meta:resourcekey="lblLeaveRequest_HourlyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                                <div class="dhtmlgoodies_contentBox" id="box_LeaveRequest_HourlyRequestOnAbsence"
                                    style="width: 70%;">
                                    <div class="dhtmlgoodies_content" id="subbox_LeaveRequest_HourlyRequestOnAbsence">
                                        <table class="BoxStyle" style="width: 100%; height: 95%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblLeaveType_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblLeaveType_HourlyRequestOnAbsence" Text=": نوع مرخصی"></asp:Label>
                                                </td>
                                                <td>&nbsp; </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <ComponentArt:CallBack ID="CallBack_cmbLeaveType_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbLeaveType_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100%">
                                                                <ClientEvents>
                                                                    <Change EventHandler="cmbLeaveType_HourlyRequestOnAbsence_onChange" />
                                                                    <Expand EventHandler="cmbLeaveType_HourlyRequestOnAbsence_onExpand" />
                                                                    <Collapse EventHandler="cmbLeaveType_HourlyRequestOnAbsence_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_LeaveTypes" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbLeaveType_HourlyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table id="tblPairsContainer_Leave_HourlyRequestOnAbsence" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 50%" valign="top">
                                                                <table style="width: 100%; height: 50px; border: 1px outset black">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblFromHour_Leave_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFromHour_Leave_HourlyRequestOnAbsence" Text=": از ساعت"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <MKB:TimeSelector ID="TimeSelector_FromHour_Leave_HourlyRequestOnAbsence" runat="server" DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                                            </MKB:TimeSelector>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table style="width: 100%; border: 1px outset black">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblToHour_Leave_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToHour_Leave_HourlyRequestOnAbsence" Text=": تا ساعت"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <MKB:TimeSelector ID="TimeSelector_ToHour_Leave_HourlyRequestOnAbsence" runat="server" DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                                            </MKB:TimeSelector>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 10%">
                                                                                        <input id="chbToHourInNextDay_Leave_HourlyRequestOnAbsence" type="checkbox" /></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblToHourInNextDay_Leave_HourlyRequestOnAbsence" runat="server" Text="روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblToHourInNextDay_Leave_HourlyRequestOnAbsence"></asp:Label>
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
                                                <td colspan="2">
                                                    <asp:Label ID="lblDescription_Leave_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDescription_Leave_HourlyRequestOnAbsence" Text=": توضیحات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <textarea id="txtDescription_Leave_HourlyRequestOnAbsence" class="TextBoxes" cols="20" name="S1" rows="2" style="width: 98%"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table style="width: 100%; border-top: gray 1px double; border-right: gray 1px double; font-size: small; border-left: gray 1px double; border-bottom: gray 1px double;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAttachment_Leave_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAttachment_Leave_HourlyRequestOnAbsence" Text="ضمیمه :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 58%">
                                                                            <ComponentArt:CallBack ID="Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence" runat="server" OnCallback="Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallBack">
                                                                                <Content>
                                                                                    <cc1:FileUploaderAJAX ID="AttachmentUploader_Leave_HourlyRequestOnAbsence" runat="server" MaxFiles="3" meta:resourcekey="AttachmentUploader_Leave_HourlyRequestOnAbsence" showDeletedFilesOnPostBack="false" text_Add="" text_Delete="" text_X="" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <CallbackComplete EventHandler="Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallBackComplete" />
                                                                                    <CallbackError EventHandler="Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                            <ComponentArt:ToolBar ID="TlbDeleteAttachment_Leave_HourlyRequestOnAbsence" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_HourlyRequestOnAbsence" runat="server" ClientSideCommand="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_HourlyRequestOnAbsence_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_HourlyRequestOnAbsence" TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                        <td id="tdAttachmentName_Leave_HourlyRequestOnAbsence"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDoctorName_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDoctorName_HourlyRequestOnAbsence" Text=": نام دکتر"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIllnessName_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblIllnessName_HourlyRequestOnAbsence" Text=": نام بیماری"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack ID="CallBack_cmbDoctorName_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbDoctorName_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="120" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="150">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbDoctorName_HourlyRequestOnAbsence_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_Doctors" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbDoctorName_HourlyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td>
                                                    <ComponentArt:CallBack ID="CallBack_cmbIllnessName_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbIllnessName_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="120" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="150">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbIllnessName_HourlyRequestOnAbsence_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_Illnesses" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbIllnessName_HourlyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="dhtmlgoodies_contentBox" id="box_MissionRequest_HourlyRequestOnAbsence"
                                    style="width: 70%;">
                                    <div class="dhtmlgoodies_content" id="subbox_MissionRequest_HourlyRequestOnAbsence">
                                        <table class="BoxStyle" style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMissionType_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblMissionType_HourlyRequestOnAbsence" Text=": نوع ماموریت"></asp:Label>
                                                </td>
                                                <td>&nbsp; </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <ComponentArt:CallBack ID="CallBack_cmbMissionType_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbMissionType_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownHeight="120" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100%">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbMissionType_HourlyRequestOnAbsence_onExpand" />
                                                                    <Collapse EventHandler="cmbMissionType_HourlyRequestOnAbsence_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_MissionTypes" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbMissionType_HourlyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1" colspan="2">
                                                    <table id="tblPairsContainer_Mission_HourlyRequestOnAbsence" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 40%" valign="top">
                                                                <table style="width: 100%; height: 50px; border: 1px outset black">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblFromHour_Mission_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFromHour_Mission_HourlyRequestOnAbsence" Text=": از ساعت"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <MKB:TimeSelector ID="TimeSelector_FromHour_Mission_HourlyRequestOnAbsence" runat="server" DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                                            </MKB:TimeSelector>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table style="width: 100%; border: 1px outset black">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblToHour_Mission_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToHour_Mission_HourlyRequestOnAbsence" Text=": تا ساعت"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <MKB:TimeSelector ID="TimeSelector_ToHour_Mission_HourlyRequestOnAbsence" runat="server" DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                                            </MKB:TimeSelector>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 10%">
                                                                                        <input id="chbToHourInNextDay_Mission_HourlyRequestOnAbsence" type="checkbox" /></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblToHourInNextDay_Mission_HourlyRequestOnAbsence" runat="server" Text="روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblToHourInNextDay_Mission_HourlyRequestOnAbsence"></asp:Label>
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
                                                <td colspan="2">
                                                    <asp:Label ID="lblDescription_Mission_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDescription_Mission_HourlyRequestOnAbsence" Text=": توضیحات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <textarea id="txtDescription_Mission_HourlyRequestOnAbsence" class="TextBoxes" cols="20" name="S2" rows="2" style="width: 98%"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table style="width: 100%; border-top: gray 1px double; border-right: gray 1px double; font-size: small; border-left: gray 1px double; border-bottom: gray 1px double;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAttachment_Mission_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAttachment_Mission_HourlyRequestOnAbsence" Text="ضمیمه :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 58%">
                                                                            <ComponentArt:CallBack ID="Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence" runat="server" OnCallback="Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallBack">
                                                                                <Content>
                                                                                    <cc1:FileUploaderAJAX ID="AttachmentUploader_Mission_HourlyRequestOnAbsence" runat="server" MaxFiles="3" meta:resourcekey="AttachmentUploader_Mission_HourlyRequestOnAbsence" showDeletedFilesOnPostBack="false" text_Add="" text_Delete="" text_X="" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <CallbackComplete EventHandler="Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallBackComplete" />
                                                                                    <CallbackError EventHandler="Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">

                                                                            <ComponentArt:ToolBar ID="TlbDeleteAttachment_Mission_HourlyRequestOnAbsence" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_HourlyRequestOnAbsence" runat="server" ClientSideCommand="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_HourlyRequestOnAbsence_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_HourlyRequestOnAbsence" TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>

                                                                        </td>
                                                                        <td id="tdAttachmentName_Mission_HourlyRequestOnAbsence"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMissionLocation_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblMissionLocation_HourlyRequestOnAbsence" Text=": محل ماموریت"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <ComponentArt:CallBack ID="CallBack_cmbMissionLocation_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbMissionLocation_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="175" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100%">
                                                                <DropDownContent>
                                                                    <ComponentArt:TreeView ID="trvMissionLocation_HourlyRequestOnAbsence" runat="server" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="95%" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19" meta:resourcekey="trvMissionLocation_HourlyRequestOnAbsence" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" Width="100%">
                                                                        <ClientEvents>
                                                                            <NodeSelect EventHandler="trvMissionLocation_HourlyRequestOnAbsence_onNodeSelect" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:TreeView>
                                                                </DropDownContent>
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbMissionLocation_HourlyRequestOnAbsence_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_MissionLocations" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbMissionLocation_HourlyRequestOnAbsence_CallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="radio" runat="server" id="rdbMissionRequest_HourlyRequestOnAbsence"
                                    name="HourlyRequestOnAbsence" onclick="rdbMissionRequest_HourlyRequestOnAbsence_onClick();" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblMissionRequest_HourlyRequestOnAbsence" Text="درخواست ماموریت"
                                    meta:resourcekey="lblMissionRequest_HourlyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 50%">
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td id="" style="color: White; font-weight: bold; font-family: Arial; width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_RegisteredRequests_HourlyRequestOnAbsence" class="HeaderLabel" style="width: 50%">Registered Requests
                                        </td>
                                        <td id="loadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td6" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridRegisteredRequests_HourlyRequestOnAbsence"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_HourlyRequestOnAbsence"
                                                        runat="server" ClientSideCommand="Refresh_GridRegisteredRequests_HourlyRequestOnAbsence();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_HourlyRequestOnAbsence"
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
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRegisteredRequests_HourlyRequestOnAbsence"
                                    OnCallback="CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallBack"
                                    Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRegisteredRequests_HourlyRequestOnAbsence" runat="server"
                                            AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="true" ShowFooter="false"
                                            FillContainer="true" FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PageSize="5" RunningMode="Client" Width="590" AllowMultipleSelect="false"
                                            AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                    DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                    HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                            HeadingText="نوع درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestType_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ ثبت درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDate_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="StatusTitle" DefaultSortDirection="Descending"
                                                            DataCellClientTemplateId="DataCellClientTemplate_clmnState_GridRegisteredRequests_HourlyRequestOnAbsence"
                                                            HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn DataField="Status" Visible="false" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnState_GridRegisteredRequests_HourlyRequestOnAbsence">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center">##GetRequestStateTitle_HourlyRequestOnAbsence(DataItem.GetMember('Status').Value)##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridRegisteredRequests_HourlyRequestOnAbsence_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RegisteredRequests" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallbackError" />
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
            runat="server" Width="300px">
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
                            <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogWaiting_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfTitle_DialogHourlyRequestOnAbsence" meta:resourcekey="hfTitle_DialogHourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfheader_AbsenceDetails_HourlyRequestOnAbsence"
            meta:resourcekey="hfheader_AbsenceDetails_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridAbsencePairs_RequestOnAbsence"
            meta:resourcekey="hfloadingPanel_GridAbsencePairs_RequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfheader_RegisteredRequests_HourlyRequestOnAbsence"
            meta:resourcekey="hfheader_RegisteredRequests_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence"
            meta:resourcekey="hfloadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_HourlyRequestOnAbsence" meta:resourcekey="hfDeleteMessage_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_HourlyRequestOnAbsence" meta:resourcekey="hfCloseMessage_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_HourlyRequestOnAbsence" meta:resourcekey="hfcmbAlarm_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfRequestStates_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfAdd_HourlyRequestOnAbsence" meta:resourcekey="hfAdd_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfDelete_HourlyRequestOnAbsence" meta:resourcekey="hfDelete_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfView_HourlyRequestOnAbsence" meta:resourcekey="hfView_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfErrorType_HourlyRequestOnAbsence" meta:resourcekey="hfErrorType_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfConnectionError_HourlyRequestOnAbsence" meta:resourcekey="hfConnectionError_HourlyRequestOnAbsence" />
    </form>
</body>
</html>
