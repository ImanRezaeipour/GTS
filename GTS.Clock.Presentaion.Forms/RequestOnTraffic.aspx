<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestOnTraffic.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.RequestOnTraffic" %>

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
<body onkeydown="RequestOnTrafficForm_onKeyDown(event);">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RequestOnTrafficForm" runat="server" meta:resourcekey="RequestOnTrafficForm"
        onsubmit="return false;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbRequestOnTraffic" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRequestOnTraffic" runat="server" ClientSideCommand="tlbItemNew_TlbRequestOnTraffic_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbRequestOnTraffic"
                                            TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRequestOnTraffic" runat="server" ClientSideCommand="tlbItemDelete_TlbRequestOnTraffic_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbRequestOnTraffic"
                                            TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRequestOnTraffic" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRequestOnTraffic" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbRequestOnTraffic_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRequestOnTraffic" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemSave_TlbRequestOnTraffic_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRequestOnTraffic"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRequestOnTraffic" runat="server" ClientSideCommand="tlbItemCancel_TlbRequestOnTraffic_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRequestOnTraffic"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRequestOnTraffic" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbRequestOnTraffic_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRequestOnTraffic"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRequestOnTraffic" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemExit_TlbRequestOnTraffic_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRequestOnTraffic"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="tdSelectedDate_RequestOnTraffic" class="HeaderLabel" style="width: 30%"></td>
                            <td runat="server" id="ActionMode_RequestOnTraffic" class="ToolbarMode" style="width: 10%"
                                meta:resourcekey="InverseAlignObj"></td>
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
                                        <td id="header_TrafficDetails_RequestOnTraffic" class="HeaderLabel" style="width: 50%">Traffic Details
                                        </td>
                                        <td id="loadingPanel_GridTrafficPairs_RequestOnTraffic" class="HeaderLabel" style="width: 45%"></td>
                                        <td id="Td5" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridTrafficPairs_RequestOnTraffic" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridTrafficPairs_RequestOnTraffic"
                                                        runat="server" ClientSideCommand="Refresh_GridTrafficPairs_RequestOnTraffic();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridTrafficPairs_RequestOnTraffic"
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
                                <ComponentArt:CallBack ID="CallBack_GridTrafficPairs_RequestOnTraffic" runat="server"
                                    OnCallback="CallBack_GridTrafficPairs_RequestOnTraffic_onCallBack" Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridTrafficPairs_RequestOnTraffic" runat="server" AllowHorizontalScrolling="true"
                                            CssClass="Grid" EnableViewState="true" ShowFooter="false" FillContainer="true"
                                            FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                            PageSize="3" RunningMode="Client" Width="590" AllowMultipleSelect="false" AllowColumnResizing="false"
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
                                                        <ComponentArt:GridColumn Align="Center" DataField="From" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridTrafficPairs_RequestOnTraffic" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="To" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridTrafficPairs_RequestOnTraffic" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <Load EventHandler="GridTrafficPairs_RequestOnTraffic_onLoad" />
                                                <ItemSelect EventHandler="GridTrafficPairs_RequestOnTraffic_onItemSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_TrafficPairs" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridTrafficPairs_RequestOnTraffic_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridTrafficPairs_RequestOnTraffic_onCallbackError" />
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
                            <td style="width: 50%">
                                <asp:Label ID="lblTrafficType_RequestOnTraffic" runat="server" Text=": نوع تردد"
                                    meta:resourcekey="lblTrafficType_RequestOnTraffic" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td style="direction: ltr;" colspan="2" rowspan="2">
                                <table id="tblPairsContainer_RequestOnTraffics" style="width: 100%;">
                                    <tr>
                                        <td style="width: 40%" valign="top">
                                            <table style="width: 100%; height:50px; border: 1px outset black">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEntrance_RequestOnTraffic" runat="server" Text=": ورود" meta:resourcekey="lblEntrance_RequestOnTraffic"
                                                            Visible="true" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <MKB:TimeSelector ID="TimeSelector_Entrance_RequestOnTraffic" runat="server" DisplaySeconds="true"
                                                            MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                        </MKB:TimeSelector>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table style="width: 100%; border: 1px outset black">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblExit_RequestOnTraffic" runat="server" Text=": خروج" meta:resourcekey="lblExit_RequestOnTraffic"
                                                            Visible="true" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%">
                                                        <MKB:TimeSelector ID="TimeSelector_Exit_RequestOnTraffic" runat="server" DisplaySeconds="true"
                                                            MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                        </MKB:TimeSelector>
                                                    </td>
                                                    <td>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <input id="chbExitInNextDay_RequestOnTraffic" type="checkbox" /></td>
                                                                <td>
                                                                    <asp:Label ID="lblExitInNextDay_RequestOnTraffic" runat="server" Text="روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblExitInNextDay_RequestOnTraffic"></asp:Label>
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
                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbTrafficType_RequestOnTraffic"
                                    OnCallback="CallBack_cmbTrafficType_RequestOnTraffic_onCallBack" Height="26">
                                    <Content>
                                        <ComponentArt:ComboBox ID="cmbTrafficType_RequestOnTraffic" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DataTextField="Name" DataValueField="ID" DropDownHeight="120" DropImageUrl="Images/ComboBox/ddn.png"
                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                            ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                            Width="270" TextBoxEnabled="true">
                                            <ClientEvents>
                                                <Change EventHandler="cmbTrafficType_RequestOnTraffic_onChange" />
                                                <Expand EventHandler="cmbTrafficType_RequestOnTraffic_onExpand" />
                                                <Collapse EventHandler="cmbTrafficType_RequestOnTraffic_onCollapse" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_TrafficTypes" />
                                    </Content>
                                    <ClientEvents>
                                        <BeforeCallback EventHandler="CallBack_cmbTrafficType_RequestOnTraffic_onBeforeCallback" />
                                        <CallbackComplete EventHandler="CallBack_cmbTrafficType_RequestOnTraffic_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_cmbTrafficType_RequestOnTraffic_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDescription_RequestOnTraffic" runat="server" Text=": توضیحات" meta:resourcekey="lblDescription_RequestOnTraffic"
                                    CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <textarea id="txtDescription_RequestOnTraffic" cols="20" name="S1" rows="2" style="height: 100%; width: 99%"
                                    class="TextBoxes"></textarea>
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
                                        <td id="header_RegisteredRequests_RequestOnTraffic" class="HeaderLabel" style="width: 50%">Registered Requests
                                        </td>
                                        <td id="loadingPanel_GridRegisteredRequests_RequestOnTraffic" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td6" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridRegisteredRequests_RequestOnTraffic" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_RequestOnTraffic"
                                                        runat="server" ClientSideCommand="Refresh_GridRegisteredRequests_RequestOnTraffic();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_RequestOnTraffic"
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
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRegisteredRequests_RequestOnTraffic"
                                    OnCallback="CallBack_GridRegisteredRequests_RequestOnTraffic_onCallBack" Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRegisteredRequests_RequestOnTraffic" runat="server"
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
                                                            HeadingText="نوع تردد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnTrafficType_GridRegisteredRequests_RequestOnTraffic" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridRegisteredRequests_RequestOnTraffic" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridRegisteredRequests_RequestOnTraffic" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ ثبت درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDate_GridRegisteredRequests_RequestOnTraffic" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="StatusTitle" DefaultSortDirection="Descending"
                                                            DataCellClientTemplateId="DataCellClientTemplate_clmnState_GridRegisteredRequests_RequestOnTraffic"
                                                            HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRegisteredRequests_RequestOnTraffic" />
                                                        <ComponentArt:GridColumn DataField="Status" Visible="false" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnState_GridRegisteredRequests_RequestOnTraffic">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center">##GetRequestStateTitle_RequestOnTraffic(DataItem.GetMember('Status').Value)##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridRegisteredRequests_RequestOnTraffic_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RegisteredRequests" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRegisteredRequests_RequestOnTraffic_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRegisteredRequests_RequestOnTraffic_onCallbackError" />
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
        <asp:HiddenField runat="server" ID="hfView_RequestOnTraffic" meta:resourcekey="hfView_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfAdd_RequestOnTraffic" meta:resourcekey="hfAdd_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfDelete_RequestOnTraffic" meta:resourcekey="hfDelete_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridTrafficPairs_RequestOnTraffic"
            meta:resourcekey="hfloadingPanel_GridTrafficPairs_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_RequestOnTraffic" meta:resourcekey="hfcmbAlarm_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRegisteredRequests_RequestOnTraffic"
            meta:resourcekey="hfloadingPanel_GridRegisteredRequests_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogRequestOnTraffic" meta:resourcekey="hfTitle_DialogRequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfheader_TrafficDetails_RequestOnTraffic" meta:resourcekey="hfheader_TrafficDetails_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfheader_RegisteredRequests_RequestOnTraffic"
            meta:resourcekey="hfheader_RegisteredRequests_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfRequestStates_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_RequestOnTraffic" meta:resourcekey="hfDeleteMessage_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RequestOnTraffic" meta:resourcekey="hfCloseMessage_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfErrorType_RequestOnTraffic" meta:resourcekey="hfErrorType_RequestOnTraffic" />
        <asp:HiddenField runat="server" ID="hfConnectionError_RequestOnTraffic" meta:resourcekey="hfConnectionError_RequestOnTraffic" />
    </form>
</body>
</html>
