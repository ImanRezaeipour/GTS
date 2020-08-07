<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KartableFilter.aspx.cs" Inherits="KartableFilter" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/upload.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/PersianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="KartableFilterForm" runat="server" meta:resourcekey="KartableFilterForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_KartableFilterForm" style="width: 100%; background-image: url('Images/Ghadir/bg-body.jpg');
        background-repeat: repeat; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label ID="lblFilterField_KartableFilter" runat="server" Text=": فیلد فیلتر"
                                            CssClass="WhiteLabel" meta:resourcekey="lblFilterField_KartableFilter"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOperator_KartableFilter" runat="server" Text=": عملگر" meta:resourcekey="lblOperator_KartableFilter"
                                            CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <ComponentArt:ComboBox ID="cmbFilterField_KartableFilter" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                            <ClientEvents>
                                                <Change EventHandler="cmbFilterField_KartableFilter_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                    </td>
                                    <td>
                                        <ComponentArt:CallBack ID="CallBack_cmbOperator_KartableFilter" runat="server" OnCallback="CallBack_cmbOperator_KartableFilter_onCallBack"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbOperator_KartableFilter" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                                </ComponentArt:ComboBox>
                                            </Content>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <div style="width: 97%" class="TabStripContainer">
                                            <ComponentArt:TabStrip ID="TabStripFilterTerms" runat="server" DefaultGroupTabSpacing="1"
                                                DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
                                                ImagesBaseUrl="images/TabStrip" MultiPageId="MultiPageFilterTerms" ScrollLeftLookId="ScrollItem"
                                                ScrollRightLookId="ScrollItem" Width="100%">
                                                <ItemLooks>
                                                    <ComponentArt:ItemLook CssClass="DefaultTab" HoverCssClass="DefaultTabHover" LabelPaddingBottom="4"
                                                        LabelPaddingLeft="15" LabelPaddingRight="15" LabelPaddingTop="4" LeftIconHeight="22"
                                                        LeftIconUrl="tab_left_icon.gif" LeftIconWidth="13" LookId="DefaultTabLook" meta:resourcekey="DefaultTabLook"
                                                        RightIconHeight="22" RightIconUrl="tab_right_icon.gif" RightIconWidth="13" />
                                                    <ComponentArt:ItemLook CssClass="SelectedTab" LabelPaddingBottom="4" LabelPaddingLeft="15"
                                                        LabelPaddingRight="15" LabelPaddingTop="4" LeftIconHeight="22" LeftIconUrl="selected_tab_left_icon.gif"
                                                        LeftIconWidth="13" LookId="SelectedTabLook" meta:resourcekey="SelectedTabLook"
                                                        RightIconHeight="22" RightIconUrl="selected_tab_right_icon.gif" RightIconWidth="13" />
                                                    <ComponentArt:ItemLook CssClass="ScrollItem" HoverCssClass="ScrollItemHover" LabelPaddingBottom="0"
                                                        LabelPaddingLeft="5" LabelPaddingRight="5" LabelPaddingTop="0" LookId="ScrollItem" />
                                                </ItemLooks>
                                                <Tabs>
                                                    <ComponentArt:TabStripTab ID="tbDate_TabStripFilterTerms" meta:resourcekey="tbDate_TabStripFilterTerms"
                                                        Text="تاریخ" Value="Date">
                                                    </ComponentArt:TabStripTab>
                                                    <ComponentArt:TabStripTab ID="tbSelective_TabStripFilterTerms" meta:resourcekey="tbSelective_TabStripFilterTerms"
                                                        Text="انتخابی" Value="Selective" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                    <ComponentArt:TabStripTab ID="tbString_TabStripFilterTerms" meta:resourcekey="tbString_TabStripFilterTerms"
                                                        Text="رشته" Value="String" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                    <ComponentArt:TabStripTab ID="tbTime_TabStripFilterTerms" meta:resourcekey="tbTime_TabStripFilterTerms"
                                                        Text="زمان" Value="Time" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                </Tabs>
                                            </ComponentArt:TabStrip>
                                        </div>
                                        <ComponentArt:MultiPage ID="MultiPageFilterTerms" runat="server" CssClass="MultiPage"
                                            Width="97%">
                                            <ComponentArt:PageView runat="server" ID="pgvDate_MultiPageFilterTerms">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDate_KartableFilter" runat="server" Text=": تاریخ" meta:resourcekey="lblDate_KartableFilter"
                                                                CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="Container_DateCalendars_KartableFilter">
                                                            <table runat="server" id="Container_pdpDate_KartableFilter" visible="false" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpDate_KartableFilter" runat="server" CssClass="PersianDatePicker"
                                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="Container_gdpDate_KartableFilter" visible="false" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalDate_KartableFilter">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpDate_KartableFilter_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpDate_KartableFilter" runat="server" ControlType="Picker"
                                                                                        PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                        SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpDate_KartableFilter_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpDate_KartableFilter" alt="" class="calendar_button" onclick="btn_gdpDate_KartableFilter_OnClick(event)"
                                                                                        onmouseup="btn_gdpDate_KartableFilter_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalDate_KartableFilter" runat="server" AllowMonthSelection="false"
                                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                            MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_KartableFilter"
                                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalDate_KartableFilter_OnChange" />
                                                                                <Load EventHandler="gCalDate_KartableFilter_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:PageView>
                                            <ComponentArt:PageView runat="server" ID="pgvSelective_MultiPageFilterTerms">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSelective_KartableFilter" runat="server" Text=": انتخابی" meta:resourcekey="lblSelective_KartableFilter"
                                                                CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ComponentArt:ComboBox ID="cmbSelective_KartableFilter" runat="server" AutoComplete="true"
                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" Width="150px">
                                                            </ComponentArt:ComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:PageView>
                                            <ComponentArt:PageView runat="server" ID="pgvString_MultiPageFilterTerms">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblString_KartableFilter" runat="server" Text=": رشته" meta:resourcekey="lblString_KartableFilter"
                                                                CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="txtString_KartableFilter" runat="server" type="text" class="TextBoxes"
                                                                style="width: 95%" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:PageView>
                                            <ComponentArt:PageView runat="server" ID="pgvTime_MultiPageFilterTerms">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTime_KartableFilter" runat="server" Text=": زمان" meta:resourcekey="lblTime_KartableFilter"
                                                                CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <MKB:TimeSelector ID="TimeSelector_Hour_KartableFilter" runat="server" DisplaySeconds="true"
                                                                MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                            </MKB:TimeSelector>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:PageView>
                                        </ComponentArt:MultiPage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 44%">
                            &nbsp;
                        </td>
                        <td style="width: 50%">
                            <table style="width: 97%;">
                                <tr>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbInterpolation_KartableFilter" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemInterpolation_TlbInterpolation_KartableFilter"
                                                    runat="server" ClientSideCommand="InterpolationConditions_KartableFilter();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemInterpolation_TlbInterpolation_KartableFilter"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td align="center">
                                        <ComponentArt:ToolBar ID="TlbApplyConditions_KartableFilter" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemApplyConditions_TlbApplyConditions_KartableFilter"
                                                    runat="server" ClientSideCommand="ApplyConditions_KartableFilter();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemApplyConditions_TlbApplyConditions_KartableFilter"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td runat="server" meta:resourcekey="InverseAlign">
                                        <ComponentArt:ToolBar ID="TlbRemoveConditions_KartableFilter" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRemoveConditions_TlbRemoveConditions_KartableFilter"
                                                    runat="server" ClientSideCommand="RemoveCondition_KartableFilter('All');" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Remove.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRemoveConditions_TlbRemoveConditions_KartableFilter"
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
                <table style="width: 98%;">
                    <tr>
                        <td style="width: 95%">
                            <table style="width: 100%; background-image: url('Images/Ghadir/bg-body.jpg'); background-repeat: repeat;
                                border: outset 1px black">
                                <tr>
                                    <td id="header_CombinationalConditions_KartableFilter" style="color: White; width: 100%">
                                        Filter Conditions
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack ID="CallBack_GridCombinationalConditions_KartableFilter" runat="server"
                                            OnCallback="CallBack_GridCombinationalConditions_KartableFilter_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridCombinationalConditions_KartableFilter" runat="server"
                                                    CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                                    ImagesBaseUrl="images/Grid/" AllowEditing="true" LoadingPanelClientTemplateId="GridCombinationalConditions_KartableFilterLoadingFeedbackTemplate"
                                                    LoadingPanelFadeDuration="1000" LoadingPanelFadeMaximumOpacity="60" LoadingPanelPosition="MiddleCenter"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="10" RunningMode="Callback"
                                                    SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false" Width="100" ShowFooter="false">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" DataKeyField="ID"
                                                            AllowSorting="false">
                                                            <Columns>
                                                                <ComponentArt:GridColumn Align="Center" DataField="ID" DefaultSortDirection="Descending"
                                                                    HeadingText="ردیف" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnNumber_GridCombinationalConditions_KartableFilter"
                                                                    Width="10" AllowEditing="False" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="FilterCondition" DefaultSortDirection="Descending"
                                                                    HeadingText="شرایط فیلتر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFilterConditions_GridCombinationalConditions_KartableFilter"
                                                                    Width="85" AllowEditing="False" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ConditionOperator" DefaultSortDirection="Descending"
                                                                    HeadingText="عملگر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOperator_GridFilterConditions_KartableFilter"
                                                                    Width="10" EditControlType="Custom" EditCellServerTemplateId="EditCellServerTemplateId_cmbInterpolationOperator_KartableFilter"
                                                                    CustomEditGetExpression="getFilter_KartableFilter(DataItem);" CustomEditSetExpression="setFilter_KartableFilter(DataItem);" />
                                                                <ComponentArt:GridColumn Align="Center" DefaultSortDirection="Descending" DataCellClientTemplateId="DataCellClientTemplateId_clmnDelete_GridFilterConditions_KartableFilter"
                                                                    HeadingText="حذف" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDelete_GridFilterConditions_KartableFilter"
                                                                    Width="10" AllowEditing="False" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientTemplates>
                                                        <ComponentArt:ClientTemplate ID="GridCombinationalConditions_KartableFilterLoadingFeedbackTemplate">
                                                            <table bgcolor="#e0e0e0" style="width: 100; height: 50">
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="font-size: 10px; font-family: Verdana;">
                                                                                    Loading...&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img alt="" border="0" height="16" src="images/Grid/spinner.gif" width="16" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ComponentArt:ClientTemplate>
                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnDelete_GridFilterConditions_KartableFilter">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td align="center">
                                                                        <img alt="" src="Images/ToolBar/cancel.png" onclick="RemoveCondition_KartableFilter('## DataItem.ClientId ##');" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ComponentArt:ClientTemplate>
                                                    </ClientTemplates>
                                                    <ServerTemplates>
                                                        <ComponentArt:GridServerTemplate ID="EditCellServerTemplateId_cmbInterpolationOperator_KartableFilter">
                                                            <Template>
                                                                <select id="cmbInterpolationOperator_KartableFilter" runat="server" style="width: 100%">
                                                                    <option value="And"></option>
                                                                    <option value="Or"></option>
                                                                </select>
                                                            </Template>
                                                        </ComponentArt:GridServerTemplate>
                                                    </ServerTemplates>
                                                    <ClientEvents>
                                                        <ItemDoubleClick EventHandler="GridCombinationalConditions_KartableFilter_onItemDoubleClick" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridCombinationalConditions_KartableFilter_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridCombinationalConditions_KartableFilter_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
    <asp:HiddenField runat="server" ID="hfErrorType_KartableFilter" meta:resourcekey="hfErrorType_KartableFilter" />
    <asp:HiddenField runat="server" ID="hfConnectionError_KartableFilter" meta:resourcekey="hfConnectionError_KartableFilter" />
    </form>
</body>
</html>
