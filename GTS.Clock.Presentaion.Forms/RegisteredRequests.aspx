<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisteredRequests.aspx.cs"
    Inherits="RegisteredRequests" %>

<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
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
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RegisteredRequestsForm" runat="server" meta:resourcekey="RegisteredRequestsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_RegisteredRequestsForm" style="width: 99%; height: 100%; font-family: Arial;
        font-size: small;" class="BoxStyle">
        <tr>
            <td colspan="2">
                <ComponentArt:ToolBar ID="TlbRegisteredRequests" runat="server" CssClass="toolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" class="BoxStyle">
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbRegisteredRequestsFilter_RegisteredRequests" runat="server"
                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemConfirmedRequests_TlbRegisteredRequestsFilter_RegisteredRequests"
                            runat="server" ClientSideCommand="tlbItemConfirmedRequests_TlbRegisteredRequestsFilter_RegisteredRequests_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirmedRequests_TlbRegisteredRequestsFilter_RegisteredRequests"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemPendingRequests_TlbRegisteredRequestsFilter_RegisteredRequests"
                            runat="server" ClientSideCommand="tlbItemPendingRequests_TlbRegisteredRequestsFilter_RegisteredRequests_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="waiting_flow.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemPendingRequests_TlbRegisteredRequestsFilter_RegisteredRequests"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemRejectedRequests_TlbRegisteredRequestsFilter_RegisteredRequests"
                            runat="server" ClientSideCommand="tlbItemRejectedRequests_TlbRegisteredRequestsFilter_RegisteredRequests_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRejectedRequests_TlbRegisteredRequestsFilter_RegisteredRequests"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemDeletedRequests_TlbRegisteredRequestsFilter_RegisteredRequests"
                            runat="server" ClientSideCommand="tlbItemDeletedRequests_TlbRegisteredRequestsFilter_RegisteredRequests_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeletedRequests_TlbRegisteredRequestsFilter_RegisteredRequests"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 30%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblYear_RegisteredRequests" runat="server" Text=": سال" CssClass="WhiteLabel"
                                            meta:resourcekey="lblYear_RegisteredRequests"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth_RegisteredRequests" runat="server" Text=": ماه" CssClass="WhiteLabel"
                                            meta:resourcekey="lblMonth_RegisteredRequests"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbYear_RegisteredRequests" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100">
                                            <ClientEvents>
                                                <Change EventHandler="cmbYear_RegisteredRequests_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                    </td>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbMonth_RegisteredRequests" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100" DropDownHeight="280">
                                            <ClientEvents>
                                                <Change EventHandler="cmbMonth_RegisteredRequests_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbView_RegisteredRequests" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_RegisteredRequests" runat="server"
                                                    ClientSideCommand="tlbItemView_TlbView_RegisteredRequests_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemView_TlbView_RegisteredRequests"
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
            <td valign="top" colspan="2">
                <table style="width: 100%; height: 300px; border: outset 1px black;" class="BoxStyle">
                    <tr>
                        <td style="height: 5%">
                            <table style="width: 100%;">
                                <tr>
                                    <td id="header_RegisteredRequests_RegisteredRequests" class="HeaderLabel" style="width: 50%;">
                                        Registered Requests
                                    </td>
                                    <td id="loadingPanel_GridRegisteredRequests_RegisteredRequests" class="HeaderLabel"
                                        style="width: 45%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridRegisteredRequests_RegisteredRequests"
                                OnCallback="CallBack_GridRegisteredRequests_RegisteredRequests_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridRegisteredRequests_RegisteredRequests" runat="server"
                                        AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="false" ShowFooter="false"
                                        FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PageSize="14" RunningMode="Client" AllowMultipleSelect="false"
                                        AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="960">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="RequestID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FlowStatus" DefaultSortDirection="Descending"
                                                        HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRegisteredRequests_RegisteredRequests"
                                                        Width="40" DataCellClientTemplateId="DataCellClientTemplateId_clmnFlowStatus_GridRegisteredRequests_RegisteredRequests" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FlowLevels" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="DataCellClientTemplateId_clmnFlowLevels_GridRegisteredRequests_RegisteredRequests"
                                                        HeadingText="مراحل" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnLevels_GridRegisteredRequests_RegisteredRequests"
                                                        Width="40" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RequestType" DefaultSortDirection="Descending"
                                                        HeadingText="نوع" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnType_GridRegisteredRequests_RegisteredRequests"
                                                        Width="30" DataCellClientTemplateId="DataCellClientTemplateId_clmnRequestType_GridRegisteredRequests_RegisteredRequests" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Row" DefaultSortDirection="Descending"
                                                        HeadingText="ردیف" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRow_GridRegisteredRequests_RegisteredRequests"
                                                        Width="30" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RequestTitle" DefaultSortDirection="Descending"
                                                        HeadingText="عنوان درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestTopic_GridRegisteredRequests_RegisteredRequests"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                        HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridRegisteredRequests_RegisteredRequests"
                                                        Width="120" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                        HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridRegisteredRequests_RegisteredRequests"
                                                        Width="120" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                        HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridRegisteredRequests_RegisteredRequests"
                                                        Width="70" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                        HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridRegisteredRequests_RegisteredRequests"
                                                        Width="70" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheDuration" DefaultSortDirection="Descending"
                                                        HeadingText="مدت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDuration_GridRegisteredRequests_RegisteredRequests"
                                                        Width="70" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                        HeadingText="شرح درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDescription_GridRegisteredRequests_RegisteredRequests"
                                                        Width="200" DataCellClientTemplateId="DataCellClientTemplateId_clmnDescription_GridRegisteredRequests_RegisteredRequests" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Applicant" DefaultSortDirection="Descending"
                                                        HeadingText="متقاضی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnApplicant_GridRegisteredRequests_RegisteredRequests"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="OperatorUser" DefaultSortDirection="Descending"
                                                        HeadingText="صادر کننده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnExporter_GridRegisteredRequests_RegisteredRequests"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                        HeadingText="تاریخ صدور" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnExportDate_GridRegisteredRequests_RegisteredRequests"
                                                        Width="100" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ManagerDescription" DefaultSortDirection="Descending"
                                                        HeadingText="توضیحات مدیر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnManagerDescription_GridRegisteredRequests_RegisteredRequests"
                                                        Width="200" DataCellClientTemplateId="DataCellClientTemplateId_clmnManagerDescription_GridRegisteredRequests_RegisteredRequests" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="AttachmentFile" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="DataCellClientTemplateId_clmnAttachmentFile_GridRegisteredRequests_RegisteredRequests"
                                                        HeadingText=" " HeadingTextCssClass="HeadingText" Width="20" />
                                                    <ComponentArt:GridColumn DataField="ManagerFlowID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonId" Visible="false" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnFlowStatus_GridRegisteredRequests_RegisteredRequests">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px;" title="##SetCellTitle_GridRegisteredRequests_RegisteredRequests('FlowStatus', DataItem.GetMember('FlowStatus').Value)##">
                                                            <img src="##SetClmnImage_GridRegisteredRequests_RegisteredRequests('FlowStatus', DataItem.GetMember('FlowStatus').Value)##"
                                                                alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnFlowLevels_GridRegisteredRequests_RegisteredRequests">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px; cursor: pointer;"
                                                            ondblclick="GetRequestFlowLevel_GridRegisteredRequests_RegisteredRequests();">
                                                            <img src="##SetClmnImage_GridRegisteredRequests_RegisteredRequests('FlowLevels', DataItem.GetMember('FlowLevels').Value)##"
                                                                alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestType_GridRegisteredRequests_RegisteredRequests">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px;" title="##SetCellTitle_GridRegisteredRequests_RegisteredRequests('RequestType', DataItem.GetMember('RequestType').Value);##">
                                                            <img src="##SetClmnImage_GridRegisteredRequests_RegisteredRequests('RequestType', DataItem.GetMember('RequestType').Value)##"
                                                                alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnDescription_GridRegisteredRequests_RegisteredRequests">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px; cursor: pointer"
                                                            ondblclick="ShowDescription_GridRegisteredRequests_RegisteredRequests('Description');">
                                                            ##DataItem.GetMember('Description').Value##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnManagerDescription_GridRegisteredRequests_RegisteredRequests">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px; cursor: pointer"
                                                            ondblclick="ShowDescription_GridRegisteredRequests_RegisteredRequests('ManagerDescription');">
                                                            ##DataItem.GetMember('ManagerDescription').Value##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnAttachmentFile_GridRegisteredRequests_RegisteredRequests">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px; cursor: pointer"
                                                            ondblclick="ShowAttachmentFile_GridRegisteredRequests_RegisteredRequests('AttachmentFile');">
                                                            ##SetAttachmentFileImage_GridRegisteredRequests_RegisteredRequests(DataItem.GetMember('AttachmentFile').Value)##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <ClientEvents>
                                            <Load EventHandler="GridRegisteredRequests_RegisteredRequests_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_RegisteredRequests" />
                                    <asp:HiddenField runat="server" ID="hfRegisteredRequestsCount_RegisteredRequests" />
                                    <asp:HiddenField runat="server" ID="hfRegisteredRequestsPageCount_RegisteredRequests" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridRegisteredRequests_RegisteredRequests_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridRegisteredRequests_RegisteredRequests_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5%">
                            <table style="width: 100%;">
                                <tr>
                                    <td runat="server" meta:resourcekey="AlignObj" style="width: 10%;">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridRegisteredRequests_RegisteredRequests" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridRegisteredRequests_RegisteredRequests"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="beginfooter_GridRegisteredRequests_RegisteredRequests" class="WhiteLabel"
                                        style="width: 45%">
                                        &nbsp;
                                    </td>
                                    <td id="endfooter_GridRegisteredRequests_RegisteredRequests" runat="server" class="WhiteLabel"
                                        meta:resourcekey="InverseAlignObj" style="width: 45%">
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
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRegisteredRequestsFilter"
        runat="server" Width="520px">
        <Content>
            <table id="tbl_RegisteredRequestsFilter_RegisteredRequests" runat="server" class="BodyStyle"
                style="width: 100%; border: outset 1px black; font-family: Arial; font-size: small">
                <tr>
                    <td id="header_Filter_RegisteredRequests" class="HeaderLabel" style="width: 98%;">
                        Filter
                    </td>
                    <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        <ComponentArt:ToolBar ID="tlbExit_RegisterteredRequests" runat="server" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly" DefaultItemTextImageSpacing="0"
                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_RegisterteredRequests" runat="server"
                                    ClientSideCommand="tlbItemExit_tlbExit_RegisterteredRequests_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_RegisterteredRequests"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2" id="Container_PersonnelSearchBox_RegisteredRequests">
                                    <table id="PersonnelSearchBox_RegisteredRequests" style="width: 100%;">
                                        <tr>
                                            <td style="width: 98%">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblPersonnel_RegisteredRequests" runat="server" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblPersonnel_RegisteredRequests" Text=": پرسنل"></asp:Label>
                                                        </td>
                                                        <td id="Td4" runat="server" meta:resourcekey="InverseAlignObj">
                                                            <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_RegisteredRequests" runat="server"
                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                Style="direction: ltr;" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_RegisteredRequests_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        TextImageSpacing="5" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_RegisteredRequests_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        TextImageSpacing="5" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_RegisteredRequests_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        TextImageSpacing="5" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_RegisteredRequests_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        TextImageSpacing="5" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_RegisteredRequests_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_RegisteredRequests"
                                                                        TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <ComponentArt:CallBack ID="CallBack_cmbPersonnel_RegisteredRequests" runat="server"
                                                    OnCallback="CallBack_cmbPersonnel_RegisteredRequests_onCallBack" Height="26">
                                                    <Content>
                                                        <ComponentArt:ComboBox ID="cmbPersonnel_RegisteredRequests" runat="server" AutoComplete="true"
                                                            AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                            DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownPageSize="7" DropDownWidth="400"
                                                            DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_RegisteredRequests"
                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                            SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                            <ClientTemplates>
                                                                <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_RegisteredRequests">
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
                                                                <table id="tblDropDownContent_cmbPersonnel_RegisteredRequests" border="0" cellpadding="0"
                                                                    cellspacing="0" width="400">
                                                                    <tr class="headingRow">
                                                                        <td id="clmnName_cmbPersonnel_RegisteredRequests" class="headingCell" style="width: 40%;
                                                                            text-align: center">
                                                                            Name And Family
                                                                        </td>
                                                                        <td id="clmnBarCode_cmbPersonnel_RegisteredRequests" class="headingCell" style="width: 30%;
                                                                            text-align: center">
                                                                            BarCode
                                                                        </td>
                                                                        <td id="clmnCardNum_cmbPersonnel_RegisteredRequests" class="headingCell" style="width: 30%;
                                                                            text-align: center">
                                                                            CardNum
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DropDownHeader>
                                                            <ClientEvents>
                                                                <Expand EventHandler="cmbPersonnel_RegisteredRequests_onExpand" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                        <asp:HiddenField ID="ErrorHiddenField_Personnel_RegisteredRequests" runat="server" />
                                                        <asp:HiddenField ID="hfPersonnelPageCount_RegisteredRequests" runat="server" />
                                                        <asp:HiddenField ID="hfPersonnelCount_RegisteredRequests" runat="server" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <BeforeCallback EventHandler="CallBack_cmbPersonnel_RegisteredRequests_onBeforeCallback" />
                                                        <CallbackComplete EventHandler="CallBack_cmbPersonnel_RegisteredRequests_onCallBackComplete" />
                                                        <CallbackError EventHandler="CallBack_cmbPersonnel_RegisteredRequests_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="txtPersonnelSearch_RegisteredRequests" runat="server" class="TextBoxes"
                                                    onclick="this.select();" onfocus="this.select();" style="width: 99%" type="text" />
                                            </td>
                                            <td>
                                                <ComponentArt:ToolBar ID="TlbSearchPersonnel_RegisteredRequests" runat="server" CssClass="toolbar"
                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_RegisteredRequests"
                                                            runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_RegisteredRequests_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_RegisteredRequests"
                                                            TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <ComponentArt:ToolBar ID="TlbAdvancedSearch_RegisteredRequests" runat="server" CssClass="toolbar"
                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_RegisteredRequests"
                                                            runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_RegisteredRequests_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_RegisteredRequests"
                                                            TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 60%">
                                    <asp:Label ID="lblRequestType_RegisteredRequests" runat="server" CssClass="WhiteLabel"
                                        meta:resourcekey="lblRequestType_RegisteredRequests" Text=": نوع درخواست"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblExporter_RegisteredRequests" runat="server" CssClass="WhiteLabel"
                                        meta:resourcekey="lblExporter_RegisteredRequests" Text="صادر کننده"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <ComponentArt:CallBack ID="CallBack_cmbRequestType_RegisteredRequests" runat="server"
                                                    OnCallback="CallBack_cmbRequestType_RegisteredRequests_onCallBack" Height="26">
                                                    <Content>
                                                        <ComponentArt:ComboBox ID="cmbRequestType_RegisteredRequests" runat="server" AutoComplete="true"
                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                            Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                            <ClientEvents>
                                                                <Expand EventHandler="cmbRequestType_RegisteredRequests_onExpand" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                        <asp:HiddenField ID="ErrorHiddenField_RequestsTypes" runat="server" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <BeforeCallback EventHandler="CallBack_cmbRequestType_RegisteredRequests_onBeforeCallback" />
                                                        <CallbackComplete EventHandler="CallBack_cmbRequestType_RegisteredRequests_onCallbackComplete" />
                                                        <CallbackError EventHandler="CallBack_cmbRequestType_RegisteredRequests_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                            <td style="width: 5%">
                                                <ComponentArt:ToolBar ID="TlbClean_cmbRequestType_RegisteredRequests" runat="server"
                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_cmbRequestType_RegisteredRequests"
                                                            runat="server" ClientSideCommand="tlbItemClean_TlbClean_cmbRequestType_RegisteredRequests_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_cmbRequestType_RegisteredRequests"
                                                            TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <ComponentArt:CallBack ID="CallBack_cmbExporter_RegisteredRequests" runat="server"
                                                    OnCallback="CallBack_cmbExporter_RegisteredRequests_onCallback" Height="26">
                                                    <Content>
                                                        <ComponentArt:ComboBox ID="cmbExporter_RegisteredRequests" runat="server" AutoComplete="true"
                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                            Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                            <ClientEvents>
                                                                <Expand EventHandler="cmbExporter_RegisteredRequests_onExpand" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                        <asp:HiddenField ID="ErrorHiddenField_Exporters" runat="server" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <BeforeCallback EventHandler="CallBack_cmbExporter_RegisteredRequests_onBeforeCallback" />
                                                        <CallbackComplete EventHandler="CallBack_cmbExporter_RegisteredRequests_onCallbackComplete" />
                                                        <CallbackError EventHandler="CallBack_cmbExporter_RegisteredRequests_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                            <td style="width: 5%">
                                                <ComponentArt:ToolBar ID="TlbClean_cmbExporter_RegisteredRequests" runat="server"
                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_cmbExporter_RegisteredRequests"
                                                            runat="server" ClientSideCommand="tlbItemClean_TlbClean_cmbExporter_RegisteredRequests_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_cmbExporter_RegisteredRequests"
                                                            TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="BoxStyle" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFromDate_RegisteredRequests" runat="server" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblFromDate_RegisteredRequests" Text=": از تاریخ"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblToDate_RegisteredRequests" runat="server" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblToDate_RegisteredRequests" Text=": تا تاریخ"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 90%;">
                                                    <tr>
                                                        <td id="Container_FromDateCalendars_RegisteredRequests">
                                                            <table runat="server" id="Container_pdpFromDate_RegisteredRequests" visible="false"
                                                                style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpFromDate_RegisteredRequests" runat="server" CssClass="PersianDatePicker"
                                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="Container_gdpFromDate_RegisteredRequests" visible="false"
                                                                style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalFromDate_RegisteredRequests" border="0" cellpadding="0"
                                                                            cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpFromDate_RegisteredRequests_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpFromDate_RegisteredRequests" runat="server" ControlType="Picker"
                                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                        SelectedDate="2008-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpFromDate_RegisteredRequests_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpFromDate_RegisteredRequests" alt="" class="calendar_button" onclick="btn_gdpFromDate_RegisteredRequests_OnClick(event)"
                                                                                        onmouseup="btn_gdpFromDate_RegisteredRequests_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalFromDate_RegisteredRequests" runat="server" AllowMonthSelection="false"
                                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_RegisteredRequests"
                                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalFromDate_RegisteredRequests_OnChange" />
                                                                                <Load EventHandler="gCalFromDate_RegisteredRequests_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 5%" valign="top">
                                                            <ComponentArt:ToolBar ID="TlbClean_FromDateCalendars_RegisteredRequests" runat="server"
                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_FromDateCalendars_RegisteredRequests"
                                                                        runat="server" ClientSideCommand="tlbItemClean_TlbClean_FromDateCalendars_RegisteredRequests_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_FromDateCalendars_RegisteredRequests"
                                                                        TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="width: 90%;">
                                                    <tr>
                                                        <td id="Container_ToDateCalendars_RegisteredRequests">
                                                            <table runat="server" id="Container_pdpToDate_RegisteredRequests" visible="false"
                                                                style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpToDate_RegisteredRequests" runat="server" CssClass="PersianDatePicker"
                                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="Container_gdpToDate_RegisteredRequests" visible="false"
                                                                style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalToDate_RegisteredRequests" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpToDate_RegisteredRequests_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpToDate_RegisteredRequests" runat="server" ControlType="Picker"
                                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                        SelectedDate="2008-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpToDate_RegisteredRequests_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpToDate_RegisteredRequests" alt="" class="calendar_button" onclick="btn_gdpToDate_RegisteredRequests_OnClick(event)"
                                                                                        onmouseup="btn_gdpToDate_RegisteredRequests_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalToDate_RegisteredRequests" runat="server" AllowMonthSelection="false"
                                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_RegisteredRequests"
                                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalToDate_RegisteredRequests_OnChange" />
                                                                                <Load EventHandler="gCalToDate_RegisteredRequests_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 5%" valign="top">
                                                            <ComponentArt:ToolBar ID="TlbClean_ToDateCalendars_RegisteredRequests" runat="server"
                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_ToDateCalendars_RegisteredRequests"
                                                                        runat="server" ClientSideCommand="TlbItemClean_TlbClean_ToDateCalendars_RegisteredRequests_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="TlbItemClean_TlbClean_ToDateCalendars_RegisteredRequests"
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
                                <td align="center" colspan="2">
                                    <ComponentArt:ToolBar ID="TlbApplyConditions_RegisteredRequests" runat="server" CssClass="toolbar"
                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                        UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemApplyConditions_TlbApplyFilterConditions_RegisteredRequests"
                                                runat="server" ClientSideCommand="tlbItemApplyConditions_TlbApplyFilterConditions_RegisteredRequests_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemApplyConditions_TlbApplyFilterConditions_RegisteredRequests"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogRegisteredRequestsFilter_OnShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRequestRegister"
        HeaderClientTemplateId="DialogRequestRegisterheader" FooterClientTemplateId="DialogRequestRegisterfooter"
        runat="server" PreloadContentUrl="false" ContentUrl="RequestRegister.aspx" IFrameCssClass="RequestRegister_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogRequestRegisterheader">
                <table id="tbl_DialogRequestRegisterheader" style="width: 823px;" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogRequestRegister.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestRegister_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogRequestRegister" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold;">
                                    </td>
                                    <td id="CloseButton_DialogRequestRegister" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogRequestRegister_IFrame').contentWindow.RequestRegister_onClose();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogRequestRegister_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogRequestRegisterfooter">
                <table id="tbl_DialogRequestRegisterfooter" style="width: 823px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestRegister_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogRequestRegister_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogRequestRegister_onShow" />
            <OnClose EventHandler="DialogRequestRegister_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogRequestDescription"
        runat="server" Width="400px">
        <Content>
            <table id="tbl_DialogRequestDescription_RegisteredRequests" runat="server" class="BodyStyle"
                style="width: 100%; font-family: Arial; font-size: small">
                <tr>
                    <td style="width: 98%">
                        &nbsp;
                    </td>
                    <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        <ComponentArt:ToolBar ID="tlbExit_RequestDescription_RegisteredRequests" runat="server"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_RequestDescription_RegisteredRequests"
                                    runat="server" ClientSideCommand="tlbItemExit_tlbExit_RequestDescription_RegisteredRequests_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_RequestDescription_RegisteredRequests"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%">
                        <asp:Label ID="lblDescription_RequestDescription_RegisteredRequests" runat="server"
                            CssClass="WhiteLabel"></asp:Label>
                    </td>
                    <td id="Td3" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtDescription_RequestDescription_RegisteredRequests" cols="5" name="S1"
                            rows="12" style="width: 99%; height: 100px;" class="TextBoxes" readonly="readonly"></textarea>
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogRequestDescription_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="320px">
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
        <ClientEvents>
            <OnShow EventHandler="DialogConfirm_OnShow" />
        </ClientEvents>
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogRegisteredRequests" meta:resourcekey="hfTitle_DialogRegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfheader_RegisteredRequests_RegisteredRequests"
        meta:resourcekey="hfheader_RegisteredRequests_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfheader_Filter_RegisteredRequests" meta:resourcekey="hfheader_Filter_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_RegisteredRequests" meta:resourcekey="hfDeleteMessage_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_RegisteredRequests" meta:resourcekey="hfCloseMessage_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfCurrentYear_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfCurrentMonth_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfRegisteredRequestsPageSize_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hffooter_GridRegisteredRequests_RegisteredRequests"
        meta:resourcekey="hffooter_GridRegisteredRequests_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfErrorType_RegisteredRequests" meta:resourcekey="hfErrorType_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfConnectionError_RegisteredRequests" meta:resourcekey="hfConnectionError_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridRegisteredRequests_RegisteredRequests"
        meta:resourcekey="hfloadingPanel_GridRegisteredRequests_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfRequestStates_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfRequestTypes_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_RegisteredRequests" meta:resourcekey="hfclmnName_cmbPersonnel_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_RegisteredRequests"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_RegisteredRequests"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="CurrentUserState_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfRequestDescription_RegisteredRequests" meta:resourcekey="hfRequestDescription_RegisteredRequests" />
    <asp:HiddenField runat="server" ID="hfManagerDescription_RegisteredRequests" meta:resourcekey="hfManagerDescription_RegisteredRequests" />
    </form>
</body>
</html>
