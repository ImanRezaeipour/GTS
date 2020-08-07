<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cartable.aspx.cs" Inherits="Cartable" %>

<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/API/DialogCartable_Operations.js"></script>
    <script type="text/javascript" src="JS/API/Cartable_onPageLoad.js"></script>
    <script type="text/javascript" src="JS/API/DialogCartableFilter_onPageLoad.js"></script>
    <script type="text/javascript" src="JS/API/DialogHistory_onPageLoad.js"></script>
    <script type="text/javascript" src="JS/API/DialogRequestsState_onPageLoad.js"></script>
    <script type="text/javascript" src="JS/API/Alert_Box.js"></script>
    <script type="text/javascript" src="JS/API/HelpForm_Operations.js"></script>
    <form id="CartableForm" runat="server" meta:resourcekey="CartableForm" onsubmit="return false;">
    <table id="Mastertbl_CartableForm" style="width: 99%; height: 100%; font-family: Arial;
        font-size: small;" class="BoxStyle">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbCartable" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                    ItemSpacing="1px" UseFadeEffect="false">
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td valign="bottom">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbCartableFilter_Cartable" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            </ComponentArt:ToolBar>
                        </td>
                        <td style="width:70%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 15%">
                                                    <asp:Label ID="lblYear_Cartable" runat="server" Text=": سال" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblYear_Cartable" Visible="false"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:Label ID="lblMonth_Cartable" runat="server" Text=": ماه" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblMonth_Cartable" Visible="false"></asp:Label>
                                                </td>
                                                <td style="width: 40%">
                                                    <asp:Label ID="lblDate_Cartable" runat="server" CssClass="WhiteLabel" Text=": تاریخ"
                                                        meta:resourcekey="lblDate_Cartable" Visible="false"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:Label ID="lblSortBy_Cartable" runat="server" Text=": مرتب سازی بر اساس" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblSortBy_Cartable"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:ComboBox ID="cmbYear_Cartable" runat="server" AutoComplete="true" AutoFilter="true"
                                                        AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                        TextBoxEnabled="false" Width="100" Visible="false">
                                                        <ClientEvents>
                                                            <Change EventHandler="cmbYear_Cartable_onChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                </td>
                                                <td>
                                                    <ComponentArt:ComboBox ID="cmbMonth_Cartable" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        TextBoxCssClass="comboTextBox" TextBoxEnabled="false" Width="100" Visible="false" DropDownHeight="280">
                                                        <ClientEvents>
                                                            <Change EventHandler="cmbMonth_Cartable_onChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                </td>
                                                <td id="Container_DateCalendars_RequestRegister">
                                                    <table id="Container_pdpDate_Cartable" runat="server" visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpDate_Cartable" runat="server" CssClass="PersianDatePicker" ReadOnly="true">
                                                                </pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Container_gdpDate_Cartable" runat="server" visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table id="Container_gCalDate_Cartable" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpDate_Cartable_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpDate_Cartable" runat="server" ControlType="Picker"
                                                                                MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                SelectedDate="2008-1-1" Visible="false">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpDate_Cartable_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpDate_Cartable" alt="" class="calendar_button" runat="server" onclick="btn_gdpDate_Cartable_OnClick(event)"
                                                                                onmouseup="btn_gdpDate_Cartable_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif"
                                                                                visible="false" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalDate_Cartable" runat="server" AllowMonthSelection="false"
                                                                    AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                    CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                    DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                    MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                    OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_Cartable"
                                                                    PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                    SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" Visible="false">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalDate_Cartable_OnChange" />
                                                                        <Load EventHandler="gCalDate_Cartable_OnLoad" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <ComponentArt:ComboBox ID="cmbSortBy_Cartable" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        TextBoxCssClass="comboTextBox" TextBoxEnabled="false" Style="width: 100%">
                                                        <ClientEvents>
                                                            <Change EventHandler="cmbSortBy_Cartable_onChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                </td>
                                                <td>
                                                    <ComponentArt:ToolBar ID="TlbView_Cartable" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                        ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_Cartable" runat="server" ClientSideCommand="tlbItemView_TlbView_Cartable_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbView_Cartable"
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
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table style="width: 100%; height: 300px;" class="BoxStyle">
                    <tr>
                        <td style="height: 5%">
                            <table style="width: 100%;">
                                <tr>
                                    <td id="header_Cartable_Cartable" class="HeaderLabel" style="width: 15%;">
                                        Cartable
                                    </td>
                                    <td id="loadingPanel_GridCartable_Cartable" class="HeaderLabel" style="width: 15%">
                                    </td>
                                    <td id="Container_SelectAllinthisPage_Cartable" style="width: 30%">
                                        <table runat="server" id="SelectAllinthisPageBox_Cartable" style="width: 100%;" visible="false">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbSelectAllinthisPage_Cartable" type="checkbox" onclick="chbSelectAllinthisPage_Cartable_onClick();" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSelectAllinthisPage_Cartable" runat="server" Text="انتخاب همه در این صفحه"
                                                        meta:resourcekey="lblSelectAllinthisPage_Cartable" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="HeaderLabel" style="width: 40%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td style="width: 80%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 30%; font-size: small; font-weight: normal;">
                                                                <asp:Label ID="lblQuickSearch_Cartable" runat="server" meta:resourcekey="lblQuickSearch_Cartable"
                                                                    Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSerchTerm_Cartable" />
                                                            </td>
                                                            <td style="width: 5%">
                                                                <ComponentArt:ToolBar ID="TlbCartableQuickSearch" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbCartableQuickSearch" runat="server"
                                                                            ClientSideCommand="tlbItemSearch_TlbCartableQuickSearch_onClick();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemSearch_TlbCartableQuickSearch" TextImageSpacing="5" />
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
                        <td valign="top">
                            <ComponentArt:CallBack ID="CallBack_GridCartable_Cartable" runat="server" OnCallback="CallBack_GridCartable_Cartable_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridCartable_Cartable" runat="server" AllowHorizontalScrolling="true"
                                        CssClass="Grid" EnableViewState="true" ShowFooter="false" FillContainer="true"
                                        FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                        PageSize="13" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                        ScrollBar="Off" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                        ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="960">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="RequestID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FlowStatus" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="DataCellClientTemplateId_clmnFlowStatus_GridCartable_Cartable"
                                                        HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridCartable_Cartable"
                                                        Width="40" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FlowLevels" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="DataCellClientTemplateId_clmnFlowLevels_GridCartable_Cartable"
                                                        HeadingText="مراحل" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnLevels_GridCartable_Cartable"
                                                        Width="40" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RequestType" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="DataCellClientTemplateId_clmnRequestType_GridCartable_Cartable"
                                                        HeadingText="نوع" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnType_GridCartable_Cartable"
                                                        Width="30" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RequestSource" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="DataCellClientTemplateId_clmnRequestSource_GridCartable_Cartable"
                                                        HeadingText="منبع" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRefrence_GridCartable_Cartable"
                                                        Width="60" />
                                                    <ComponentArt:GridColumn Align="Center" DefaultSortDirection="Descending" ColumnType="CheckBox"
                                                        HeadingText="انتخاب" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnSelect_GridCartable_Cartable"
                                                        Width="50" AllowEditing="True" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Row" DefaultSortDirection="Descending"
                                                        HeadingText="ردیف" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRow_GridCartable_Cartable"
                                                        Width="30" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="BarCode" DefaultSortDirection="Descending"
                                                        HeadingText="کد پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonnelBarCode_GridCartable_Cartable"
                                                        Width="120" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Applicant" DefaultSortDirection="Descending"
                                                        HeadingText="درخواست کننده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnApplicant_GridCartable_Cartable"
                                                        Width="150" DataCellClientTemplateId="DataCellClientTemplateId_clmnApplicant_GridCartable_Cartable" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RequestTitle" DefaultSortDirection="Descending"
                                                        HeadingText="عنوان درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestTopic_GridCartable_Cartable"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                        HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridCartable_Cartable"
                                                        Width="100" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                        HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridCartable_Cartable"
                                                        Width="100" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                        HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridCartable_Cartable"
                                                        Width="70" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                        HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridCartable_Cartable"
                                                        Width="70" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                        HeadingText="تاریخ صدور" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnExportDate_GridCartable_Cartable"
                                                        Width="100" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="OperatorUser" DefaultSortDirection="Descending"
                                                        HeadingText="صادر کننده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnExporter_GridCartable_Cartable"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                        HeadingText="شرح درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDescription_GridCartable_Cartable"
                                                        DataCellClientTemplateId="DataCellClientTemplateId_clmnDescription_GridCartable_Cartable"
                                                        Width="200" />
                                                    <ComponentArt:GridColumn DataField="ManagerFlowID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonId" Visible="false" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnFlowStatus_GridCartable_Cartable">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px;" title="##SetCellTitle_GridCartable_Cartable('FlowStatus', DataItem.GetMember('FlowStatus').Value)##">
                                                            <img src="##SetClmnImage_GridCartable_Cartable('FlowStatus', DataItem.GetMember('FlowStatus').Value)##"
                                                                alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnFlowLevels_GridCartable_Cartable">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px; cursor: pointer;"
                                                            ondblclick="GetRequestFlowLevel_GridCartable_Cartable();">
                                                            <img src="##SetClmnImage_GridCartable_Cartable('FlowLevels', DataItem.GetMember('FlowLevels').Value)##"
                                                                alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestType_GridCartable_Cartable">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px;" title="##SetCellTitle_GridCartable_Cartable('RequestType', DataItem.GetMember('RequestType').Value);##">
                                                            <img src="##SetClmnImage_GridCartable_Cartable('RequestType', DataItem.GetMember('RequestType').Value)##"
                                                                alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestSource_GridCartable_Cartable">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px;" title="##SetCellTitle_GridCartable_Cartable('RequestSource', DataItem.GetMember('RequestSource').Value);##">
                                                            <img src="##SetClmnImage_GridCartable_Cartable('RequestSource', DataItem.GetMember('RequestSource').Value)##"
                                                                alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnDescription_GridCartable_Cartable">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px; cursor: pointer"
                                                            ondblclick="ShowRequestDescription_GridCartable_Cartable();">
                                                            ##DataItem.GetMember('Description').Value##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnApplicant_GridCartable_Cartable">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px; cursor: pointer"
                                                            ondblclick="ShowApplicantImage_GridCartable_Cartable();">
                                                            ##DataItem.GetMember('Applicant').Value##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <ClientEvents>
                                            <Load EventHandler="GridCartable_Cartable_onLoad" />
                                            <ItemCheckChange EventHandler="GridCartable_Cartable_onItemCheckChange" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Cartable" />
                                    <asp:HiddenField runat="server" ID="hfCartableCount_Cartable" />
                                    <asp:HiddenField runat="server" ID="hfCartablePageCount_Cartable" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridCartable_Cartable_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridCartable_Cartable_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5%">
                            <table style="width: 100%;">
                                <tr>
                                    <td runat="server" meta:resourcekey="AlignObj" style="width: 10%;">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridCartable_Cartable" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridCartable_Cartable" runat="server"
                                                    ClientSideCommand="tlbItemRefresh_TlbPaging_GridCartable_Cartable_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridCartable_Cartable"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridCartable_Cartable" runat="server"
                                                    ClientSideCommand="tlbItemFirst_TlbPaging_GridCartable_Cartable_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="first.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridCartable_Cartable"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridCartable_Cartable" runat="server"
                                                    ClientSideCommand="tlbItemBefore_TlbPaging_GridCartable_Cartable_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridCartable_Cartable"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridCartable_Cartable" runat="server"
                                                    ClientSideCommand="tlbItemNext_TlbPaging_GridCartable_Cartable_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="Next.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridCartable_Cartable"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridCartable_Cartable" runat="server"
                                                    ClientSideCommand="tlbItemLast_TlbPaging_GridCartable_Cartable_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="last.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridCartable_Cartable"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="beginfooter_GridCartable_Cartable" class="WhiteLabel" style="width: 45%">
                                        &nbsp;
                                    </td>
                                    <td runat="server" id="endfooter_GridCartable_Cartable" class="WhiteLabel" style="width: 45%"
                                        meta:resourcekey="InverseAlignObj">
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
        HeaderClientTemplateId="DialogCartableFilterheader" FooterClientTemplateId="DialogCartableFilterfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogCartableFilter"
        runat="server" PreloadContentUrl="false" ContentUrl="CartableFilter.aspx" IFrameCssClass="CartableFilter_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogCartableFilterheader">
                <table id="tbl_DialogCartableFilterheader" style="width: 603px" cellpadding="0" cellspacing="0"
                    border="0" onmousedown="DialogCartableFilter.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogCartableFilter_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogCartableFilter" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogCartableFilter" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="DialogCartableFilter.Close('cancelled')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogCartableFilter_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogCartableFilterfooter">
                <table id="tbl_DialogCartableFilterfooter" style="width: 603px" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogCartableFilter_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogCartableFilter_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogCartableFilter_onShow" />
            <OnClose EventHandler="DialogCartableFilter_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogHistoryheader" FooterClientTemplateId="DialogHistoryfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogHistory"
        runat="server" PreloadContentUrl="false" ContentUrl="History.aspx" IFrameCssClass="History_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogHistoryheader">
                <table id="tbl_DialogHistoryheader" style="width: 603px" cellpadding="0" cellspacing="0"
                    border="0" onmousedown="DialogHistory.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogHistory_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogHistory" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogHistory" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogHistory_IFrame').src = 'WhitePage.aspx'; DialogHistory.Close('cancelled')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogHistory_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogHistoryfooter">
                <table id="tbl_DialogHistoryfooter" style="width: 603px" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogHistory_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogHistory_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogHistory_onShow" />
            <OnClose EventHandler="DialogHistory_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogRequestRejectDescription"
        runat="server" Width="350px">
        <Content>
            <table id="tbl_RequestRejectDescription_Cartable" runat="server" style="width: 100%;
                font-family: Arial; font-size: small" class="BodyStyle">
                <tr>
                    <td style="width: 98%">
                        <ComponentArt:ToolBar ID="TlbRequestReject_Cartable" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbRequestReject_Cartable" runat="server"
                                    ClientSideCommand="tlbItemEndorsement_TlbRequestReject_Cartable_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbRequestReject_Cartable"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRequestReject_Cartable" runat="server"
                                    ClientSideCommand="tlbItemCancel_TlbRequestReject_Cartable_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRequestReject_Cartable"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                    <td runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        <ComponentArt:ToolBar ID="tlbExit_RequestReject_Cartable" runat="server" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly" DefaultItemTextImageSpacing="0"
                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_RequestReject_Cartable" runat="server"
                                    ClientSideCommand="tlbItemExit_tlbExit_RequestReject_Cartable_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_RequestReject_Cartable"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td id="hfDescription_RequestReject_Cartable" style="width: 98%">
                    </td>
                    <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtDescription_RequestReject_Cartable" cols="5" name="S1" rows="12"
                            style="width: 99%; height: 100px;" class="TextBoxes"></textarea>
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogRequestRejectDescription_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogRequestDescription"
        runat="server" Width="400px">
        <Content>
            <table id="tbl_DialogRequestDescription_Cartable" runat="server" class="BodyStyle"
                style="width: 100%; font-family: Arial; font-size: small">
                <tr>
                    <td style="width: 98%">
                        &nbsp;
                    </td>
                    <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        <ComponentArt:ToolBar ID="tlbExit_RequestDescription_Cartable" runat="server" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly" DefaultItemTextImageSpacing="0"
                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_RequestDescription_Cartable" runat="server"
                                    ClientSideCommand="tlbItemExit_tlbExit_RequestDescription_Cartable_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_RequestDescription_Cartable"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%">
                        <asp:Label ID="lblDescription_RequestDescription_Cartable" runat="server" CssClass="WhiteLabel"
                            meta:resourcekey="lblDescription_RequestDescription_Cartable" Text=": توضیحات درخواست"></asp:Label>
                    </td>
                    <td id="Td3" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtDescription_RequestDescription_Cartable" cols="5" name="S1" rows="12"
                            style="width: 99%; height: 100px" class="TextBoxes" readonly="readonly"></textarea>
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogRequestDescription_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogRequestsStateheader" FooterClientTemplateId="DialogRequestsStatefooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRequestsState"
        runat="server" PreloadContentUrl="false" ContentUrl="RequestsState.aspx" IFrameCssClass="RequestsState_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogRequestsStateheader">
                <table id="tbl_DialogRequestsStateheader" style="width: 603px" cellpadding="0" cellspacing="0"
                    border="0" onmousedown="DialogRequestsState.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestsState_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogRequestsState" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogRequestsState" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="DialogRequestsState.Close('cancelled')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogRequestsState_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogRequestsStatefooter">
                <table id="tbl_DialogRequestsStatefooter" style="width: 603px" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestsState_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogRequestsState_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogRequestsState_onShow" />
            <OnClose EventHandler="DialogRequestsState_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogApplicantImage"
        runat="server" Width="200px">
        <Content>
            <table id="Mastertbl_DialogApplicantImage" style="width: 100%" class="BodyStyle">
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="width:95%" id="tdCurrentApplicant_DialogApplicantImage">
                                    &nbsp;
                                </td>
                                <td id="Td4" style="width: 5%" runat="server" meta:resourcekey="InverseAlignObj">
                                    <ComponentArt:ToolBar ID="TlbApplicantPicture" runat="server" 
                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemClosePicture_TlbApplicantPicture" runat="server"
                                                ClientSideCommand="tlbItemClosePicture_TlbApplicantPicture_onClick();" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemClosePicture_TlbApplicantPicture"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <iframe id="ApplicantImage_DialogPersonnelMainInformation" runat="server" scrolling="yes"
                            style="width: 70%; height: 170px; overflow: scroll" allowtransparency="true" frameborder="0"></iframe>
                    </td>
                </tr>
            </table>
        </Content>
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogCartable" meta:resourcekey="hfTitle_DialogCartable" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogSurveyedRequests" meta:resourcekey="hfTitle_DialogSurveyedRequests" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogSentry" meta:resourcekey="hfTitle_DialogSentry" />
    <asp:HiddenField runat="server" ID="hfheader_Cartable_Cartable" meta:resourcekey="hfheader_Cartable_Cartable" />
    <asp:HiddenField runat="server" ID="hfheader_SurveyedRequests_Cartable" meta:resourcekey="hfheader_SurveyedRequests_Cartable" />
    <asp:HiddenField runat="server" ID="hfheader_SentryCartable_Cartable" meta:resourcekey="hfheader_SentryCartable_Cartable" />
    <asp:HiddenField runat="server" ID="hfCartablePageSize_Cartable" />
    <asp:HiddenField runat="server" ID="hffooter_GridCartable_Cartable" meta:resourcekey="hffooter_GridCartable_Cartable" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridCartable_Cartable" meta:resourcekey="hfloadingPanel_GridCartable_Cartable" />
    <asp:HiddenField runat="server" ID="hfRejectMessage_Cartable" meta:resourcekey="hfRejectMessage_Cartable" />
    <asp:HiddenField runat="server" ID="hfCartableCloseMessage_Cartable" meta:resourcekey="hfCartableCloseMessage_Cartable" />
    <asp:HiddenField runat="server" ID="hfSurveyCloseMessage_Cartable" meta:resourcekey="hfSurveyCloseMessage_Cartable" />
    <asp:HiddenField runat="server" ID="hfSentryCloseMessage_Cartable" meta:resourcekey="hfSentryCloseMessage_Cartable" />
    <asp:HiddenField runat="server" ID="hfPageChange_Cartable" meta:resourcekey="hfPageChange_Cartable" />
    <asp:HiddenField runat="server" ID="hfRequestTypes_Cartable" />
    <asp:HiddenField runat="server" ID="hfRequestSources_Cartable" />
    <asp:HiddenField runat="server" ID="hfRequestStates_Cartable" />
    <asp:HiddenField runat="server" ID="hfCurrentYear_Cartable" />
    <asp:HiddenField runat="server" ID="hfCurrentMonth_Cartable" />
    <asp:HiddenField runat="server" ID="hfCurrentSortBy_Cartable" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_Cartable" />
    <asp:HiddenField runat="server" ID="hfErrorType_Cartable" meta:resourcekey="hfErrorType_Cartable" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Cartable" meta:resourcekey="hfConnectionError_Cartable" />
    <asp:HiddenField runat="server" ID="hfRequestRejectDescription_Cartable" meta:resourcekey="hfRequestRejectDescription_Cartable" />
    <asp:HiddenField runat="server" ID="hfRequestDeleteDescription_Cartable" meta:resourcekey="hfRequestDeleteDescription_Cartable" />
    </form>
</body>
</html>
