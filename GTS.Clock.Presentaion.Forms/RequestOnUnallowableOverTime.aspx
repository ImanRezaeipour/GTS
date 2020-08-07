<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestOnUnallowableOverTime.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.RequestOnUnallowableOverTime" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body onkeydown="RequestOnUnallowableOverTimeForm_onKeyDown(event);">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RequestOnUnallowableOverTimeForm" runat="server" meta:resourcekey="RequestOnUnallowableOverTimeForm"
        onsubmit="return false;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; height: 400px; font-family: Arial; font-size: small;"
            class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbRequestOnUnallowableOverTime" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRequestOnUnallowableOverTime" runat="server"
                                            ClientSideCommand="tlbItemNew_TlbRequestOnUnallowableOverTime_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemNew_TlbRequestOnUnallowableOverTime"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRequestOnUnallowableOverTime" runat="server"
                                            ClientSideCommand="tlbItemDelete_TlbRequestOnUnallowableOverTime_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbRequestOnUnallowableOverTime"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRequestOnUnallowableOverTime" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRequestOnUnallowableOverTime"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbRequestOnUnallowableOverTime_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRequestOnUnallowableOverTime" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbRequestOnUnallowableOverTime_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbRequestOnUnallowableOverTime"
                                            TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRequestOnUnallowableOverTime" runat="server"
                                            ClientSideCommand="tlbItemCancel_TlbRequestOnUnallowableOverTime_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRequestOnUnallowableOverTime"
                                            TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRequestOnUnallowableOverTime"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbRequestOnUnallowableOverTime_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRequestOnUnallowableOverTime"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRequestOnUnallowableOverTime" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemExit_TlbRequestOnUnallowableOverTime_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbRequestOnUnallowableOverTime"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="tdSelectedDate_RequestOnUnallowableOverTime" class="HeaderLabel" style="width: 30%"></td>
                            <td runat="server" id="ActionMode_RequestOnUnallowableOverTime" class="ToolbarMode"
                                style="width: 10%" meta:resourcekey="InverseAlignObj"></td>
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
                                        <td id="header_OverTimeDetails_RequestOnUnallowableOverTime" class="HeaderLabel"
                                            style="width: 50%">OverTime Details
                                        </td>
                                        <td id="loadingPanel_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td5" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime"
                                                        runat="server" ClientSideCommand="Refresh_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime"
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
                                <ComponentArt:CallBack ID="CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime"
                                    runat="server" OnCallback="CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onCallBack"
                                    Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridUnallowableOverTimePairs_RequestOnUnallowableOverTime"
                                            runat="server" AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="true"
                                            ShowFooter="false" FillContainer="true" FooterCssClass="GridFooter" Height="150"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PageSize="3" RunningMode="Client"
                                            Width="590" AllowMultipleSelect="false" AllowColumnResizing="false" ScrollBar="On"
                                            ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                            ScrollImagesFolderUrl="images/Grid/scroller/" ScrollButtonWidth="16" ScrollButtonHeight="17"
                                            ScrollBarCssClass="ScrollBar" ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                    DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                    HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="From" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="To" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <Load EventHandler="GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onLoad" />
                                                <ItemSelect EventHandler="GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onItemSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_UnallowableOverTimePairs" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onCallbackError" />
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
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblOverTimeType_RequestOnUnallowableOverTime" runat="server" Text=": نوع اضافه کاری"
                                                meta:resourcekey="lblOverTimeType_RequestOnUnallowableOverTime" Visible="true"
                                                CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <table id="tblPairsContainer_RequestOnUnallowableOverTime" style="width: 100%;">
                                                <tr>
                                                    <td style="width: 40%" valign="top">
                                                        <table style="width: 100%; height: 50px; border: 1px outset black">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblFromHour_RequestOnUnallowableOverTime" runat="server" Text=": ورود"
                                                                        meta:resourcekey="lblFromHour_RequestOnUnallowableOverTime" Visible="true" CssClass="WhiteLabel"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <MKB:TimeSelector ID="TimeSelector_FromHour_RequestOnUnallowableOverTime" runat="server"
                                                                        DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                        Visible="true">
                                                                    </MKB:TimeSelector>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table style="width: 100%; border: 1px outset black">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblToHour_RequestOnUnallowableOverTime" runat="server" Text=": خروج"
                                                                        meta:resourcekey="lblToHour_RequestOnUnallowableOverTime" Visible="true" CssClass="WhiteLabel"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <MKB:TimeSelector ID="TimeSelector_ToHour_RequestOnUnallowableOverTime" runat="server"
                                                                        DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                        Visible="true">
                                                                    </MKB:TimeSelector>
                                                                </td>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 10%">
                                                                                <input id="chbToHourInNextDay_RequestOnUnallowableOverTime" type="checkbox" disabled="disabled"/></td>
                                                                            <td>
                                                                                <asp:Label ID="lblToHourInNextDay_RequestOnUnallowableOverTime" runat="server" Text="روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblToHourInNextDay_RequestOnUnallowableOverTime"></asp:Label>
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
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbOverTimeType_RequestOnUnallowableOverTime"
                                                OnCallback="CallBack_cmbOverTimeType_RequestOnUnallowableOverTime_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbOverTimeType_RequestOnUnallowableOverTime" runat="server"
                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                        DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropDownHeight="120" DropImageUrl="Images/ComboBox/ddn.png"
                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                        Width="270" TextBoxEnabled="true" Enabled="false">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbOverTimeType_RequestOnUnallowableOverTime_onExpand" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_OverTimeTypes" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBack_cmbOverTimeType_RequestOnUnallowableOverTime_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBack_cmbOverTimeType_RequestOnUnallowableOverTime_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbOverTimeType_RequestOnUnallowableOverTime_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDescription_RequestOnUnallowableOverTime" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblDescription_RequestOnUnallowableOverTime" Text=": توضیحات"
                                    Visible="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <textarea id="txtDescription_RequestOnUnallowableOverTime" cols="20" name="S1" rows="2"
                                    style="height: 100%; width: 99%" class="TextBoxes"></textarea>
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
                                        <td id="header_RegisteredRequests_RequestOnUnallowableOverTime" class="HeaderLabel"
                                            style="width: 50%">Registered Requests
                                        </td>
                                        <td id="loadingPanel_GridRegisteredRequests_RequestOnUnallowableOverTime" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td6" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridRegisteredRequests_RequestOnUnallowableOverTime"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_RequestOnUnallowableOverTime"
                                                        runat="server" ClientSideCommand="Refresh_GridRegisteredRequests_RequestOnUnallowableOverTime();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_RequestOnUnallowableOverTime"
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
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime"
                                    OnCallback="CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime_onCallBack"
                                    Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRegisteredRequests_RequestOnUnallowableOverTime" runat="server"
                                            AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="true" ShowFooter="false"
                                            FillContainer="true" FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PageSize="3" RunningMode="Client" Width="590" AllowMultipleSelect="false"
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
                                                            HeadingText="نوع درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestType_GridRegisteredRequests_RequestOnUnallowableOverTime" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridRegisteredRequests_RequestOnUnallowableOverTime" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridRegisteredRequests_RequestOnUnallowableOverTime" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاربخ ثبت درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDate_GridRegisteredRequests_RequestOnUnallowableOverTime" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="StatusTitle" DefaultSortDirection="Descending"
                                                            DataCellClientTemplateId="DataCellClientTemplate_clmnState_GridRegisteredRequests_RequestOnUnallowableOverTime"
                                                            HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRegisteredRequests_RequestOnUnallowableOverTime" />
                                                        <ComponentArt:GridColumn DataField="Status" Visible="false" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnState_GridRegisteredRequests_RequestOnUnallowableOverTime">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center">##GetRequestStateTitle_RequestOnUnallowableOverTime(DataItem.GetMember('Status').Value)##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridRegisteredRequests_RequestOnUnallowableOverTime_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RegisteredRequests" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime_onCallbackError" />
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
        <asp:HiddenField runat="server" ID="hfView_RequestOnUnallowableOverTime" meta:resourcekey="hfView_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfAdd_RequestOnUnallowableOverTime" meta:resourcekey="hfAdd_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfDelete_RequestOnUnallowableOverTime" meta:resourcekey="hfDelete_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime"
            meta:resourcekey="hfloadingPanel_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_RequestOnUnallowableOverTime" meta:resourcekey="hfcmbAlarm_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRegisteredRequests_RequestOnUnallowableOverTime"
            meta:resourcekey="hfloadingPanel_GridRegisteredRequests_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogRequestOnUnallowableOverTime" meta:resourcekey="hfTitle_DialogRequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfheader_OverTimeDetails_RequestOnUnallowableOverTime"
            meta:resourcekey="hfheader_OverTimeDetails_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfheader_RegisteredRequests_RequestOnUnallowableOverTime"
            meta:resourcekey="hfheader_RegisteredRequests_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfRequestStates_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_RequestOnUnallowableOverTime"
            meta:resourcekey="hfDeleteMessage_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RequestOnUnallowableOverTime"
            meta:resourcekey="hfCloseMessage_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfErrorType_RequestOnUnallowableOverTime" meta:resourcekey="hfErrorType_RequestOnUnallowableOverTime" />
        <asp:HiddenField runat="server" ID="hfConnectionError_RequestOnUnallowableOverTime"
            meta:resourcekey="hfConnectionError_RequestOnUnallowableOverTime" />
    </form>
</body>
</html>
