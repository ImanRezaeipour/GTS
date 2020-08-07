<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefineUiValidation.aspx.cs"
    Inherits="DefineUiValidation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/toolbar.css" type="text/css" rel="stylesheet" />
    <link href="css/gridStyle.css" type="text/css" rel="stylesheet" />
    <link href="css/tabStyle.css" type="text/css" rel="stylesheet" />
    <link href="css/calendarStyle.css" type="text/css" rel="stylesheet" />
    <link href="css/multiPage.css" type="text/css" rel="stylesheet" />
    <link href="css/style.css" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/API/Alert_Box.js"></script>
    <script type="text/javascript" src="JS/API/DropDownDive.js"></script>
    <script type="text/javascript" src="JS/API/DefineUiValidation_onPageLoad.js"></script>
    <script type="text/javascript" src="JS/API/DialogDefineUiValidation_Operations.js"></script>
    <form id="DefineUiValidationForm" runat="server" meta:resourcekey="DefineUiValidationForm">
    <table style="width: 100%; font-family: Arial; font-size: small" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbDefineUiValidation" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDefineUiValidation" runat="server" ClientSideCommand="tlbItemSave_TlbDefineUiValidation_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbDefineUiValidation"
                                        TextImageSpacing="5" />
                                    <%--<ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDefineUiValidation" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemCancel_TlbDefineUiValidation_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDefineUiValidation"
                                            TextImageSpacing="5" Visible="true" />--%>
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbDefineUiValidation" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemHelp_TlbDefineUiValidation_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbDefineUiValidation"
                                        TextImageSpacing="5" Visible="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbDefineUiValidation" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbDefineUiValidation_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbDefineUiValidation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbDefineUiValidation" runat="server" ClientSideCommand="tlbItemExit_TlbDefineUiValidation_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbDefineUiValidation"
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
                <table style="width: 50%;">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblNameDefineUiValidation_DefineUiValidation" runat="server" CssClass="WhiteLabel"
                                meta:resourcekey="lblNameDefineUiValidation_DefineUiValidation" Text=": نام واسط کاربری"></asp:Label>
                        </td>
                        <td style="width: 70%">
                            <input id="txtNameDefineUiValidation_DefineUiValidation" runat="server" class="TextBoxes"
                                disabled="disabled" style="width: 95%;" type="text" onclick="this.select();"
                                onfocus="this.select();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 90%" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_DefineUiValidation_DefineUiValidation" class="HeaderLabel" style="width: 50%">
                                        Define UiValidation
                                    </td>
                                    <td id="loadingPanel_GridDefineUiValidation_DefineUiValidation" class="HeaderLabel"
                                        style="width: 45%">
                                    </td>
                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridDefineUiValidation_DefineUiValidation" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridDefineUiValidation_DefineUiValidation"
                                                    runat="server" ClientSideCommand="Refresh_GridDefineUiValidation_DefineUiValidation();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridDefineUiValidation_DefineUiValidation"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridDefineUiValidation_DefineUiValidation"
                                OnCallback="CallBack_GridDefineUiValidation_DefineUiValidation_Callback">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridDefineUiValidation_DefineUiValidation" runat="server"
                                        CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                        PageSize="5" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
                                        AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" ScrollBar="On"
                                        ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                        ScrollImagesFolderUrl="images/Grid/scroller/" ScrollButtonWidth="16" ScrollButtonHeight="17"
                                        ScrollBarCssClass="ScrollBar" ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="RuleID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Active" DefaultSortDirection="Descending"
                                                        HeadingText="فعال" ColumnType="CheckBox" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDefineUiValidationActive_GridDefineUiValidation"
                                                        Width="60" AllowEditing="True" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RuleName" DefaultSortDirection="Descending"
                                                        HeadingText="متن قانون" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDefineUiValidationContent_GridDefineUiValidation"
                                                        Width="310" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="OpratorRestriction" DefaultSortDirection="Descending"
                                                        HeadingText="اعمال اپراتور" ColumnType="CheckBox" HeadingTextCssClass="HeadingText"
                                                        meta:resourcekey="clmnDefineUiValidationAction_GridDefineUiValidation" Width="100"
                                                        AllowEditing="True" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <ItemSelect EventHandler="GridDefineUiValidation_DefineUiValidation_onItemSelect" />
                                            <Load EventHandler="GridDefineUiValidation_DefineUiValidation_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField ID="ErrorHiddenField_DefineUiValidation" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridDefineUiValidation_DefineUiValidation_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridDefineUiValidation_DefineUiValidation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <table style="width: 100%">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbParameterDefineUiValidation" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbParameterDefineUiValidation" runat="server"
                                        ClientSideCommand="tlbItemSave_TlbParameterDefineUiValidation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbParameterDefineUiValidation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbParameterDefineUiValidation" runat="server"
                                        DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbDefineUiValidation_onClick();"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbParameterDefineUiValidation"
                                        TextImageSpacing="5" Visible="true" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <table style="width: 100%" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_ParameterDefineUiValidation_DefineUiValidation" class="HeaderLabel"
                                                    style="width: 50%">
                                                    parameter UiValidation
                                                </td>
                                                <td id="loadingPanel_GridParameterDefineUiValidation_DefineUiValidation" class="HeaderLabel"
                                                    style="width: 45%">
                                                </td>
                                                <td id="Td4" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <%--<ComponentArt:ToolBar ID="TlbRefresh_GridParameterDefineUiValidation_DefineUiValidation" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" 
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridParameterDefineUiValidation_DefineUiValidation" runat="server"
                                                                ClientSideCommand="Refresh_GridParameterDefineUiValidation_DefineUiValidation();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridParameterDefineUiValidation_DefineUiValidation"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>--%>
                                                </td>
                                            </tr>
                                        </table>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridParameterDefineUiValidation_DefineUiValidation"
                                            OnCallback="CallBack_GridParameterDefineUiValidation_DefineUiValidation_Callback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridParameterDefineUiValidation_DefineUiValidation" runat="server"
                                                    CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                                    ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                                    PageSize="5" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
                                                    AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" ScrollBar="On"
                                                    ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                                    ScrollImagesFolderUrl="images/Grid/scroller/" ScrollButtonWidth="16" ScrollButtonHeight="17"
                                                    ScrollBarCssClass="ScrollBar" ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="Type" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="KeyName" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="پارامتر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnParameterDefineUiValidationParameter_GridDefineUiValidation"
                                                                    Width="150" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="TheValue" DefaultSortDirection="Descending"
                                                                    HeadingText="مقدار" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnParameterDefineUiValidationValue_GridDefineUiValidation"
                                                                    Width="200" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridParameterDefineUiValidation_DefineUiValidation_onItemSelect" />
                                                        <Load EventHandler="GridParameterDefineUiValidation_DefineUiValidation_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_ParameterDefineUiValidation" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridParameterDefineUiValidation_DefineUiValidation_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridParameterDefineUiValidation_DefineUiValidation_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table style="width: 50%">
                                <tr>
                                    <td>
                                        <div style="width: 100%" class="TabStripContainer">
                                            <ComponentArt:TabStrip ID="TabStripRuleParametersTerms" runat="server" DefaultGroupTabSpacing="1"
                                                DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
                                                ImagesBaseUrl="images/TabStrip" MultiPageId="MultiPageRuleParametersTerms" ScrollLeftLookId="ScrollItem"
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
                                                    <ComponentArt:TabStripTab ID="tbNumeric_TabStripRuleParametersTerms" meta:resourcekey="tbNumeric_TabStripRuleParametersTerms"
                                                        Text="عددی" Value="Numeric" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                    <ComponentArt:TabStripTab ID="tbTime_TabStripRuleParametersTerms" meta:resourcekey="tbTime_TabStripRuleParametersTerms"
                                                        Text="زمان" Value="Time" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                    <ComponentArt:TabStripTab ID="tbDate_TabStripRuleParametersTerms" meta:resourcekey="tbDate_TabStripRuleParametersTerms"
                                                        Text="تاریخ" Value="Date" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                </Tabs>
                                            </ComponentArt:TabStrip>
                                        </div>
                                        <ComponentArt:MultiPage ID="MultiPageRuleParametersTerms" runat="server" CssClass="MultiPage"
                                            Width="300">
                                            <ComponentArt:PageView ID="pgvNumeric_MultiPageRuleParametersTerms" runat="server"
                                                Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblNumeric_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblNumeric_RuleParameters"
                                                                Text=": عددی"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <input id="txtNumeric_RuleParameters" runat="server" type="text" class="TextBoxes"
                                                                            style="width: 85px;" disabled="disabled" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <ComponentArt:ToolBar ID="TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms"
                                                                                    runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                                    Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:PageView>
                                            <ComponentArt:PageView ID="pgvTime_MultiPageRuleParametersTerms" runat="server" Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTime_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTime_RuleParameters"
                                                                Text=": زمان"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <%--<MKB:TimeSelector ID="TimeSelector_Hour_RuleParameters" runat="server" DisplaySeconds="true"
                                                    MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                </MKB:TimeSelector>--%>
                                                                        <table dir="ltr">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <input type="text" id="TimeSelector_Hour_RuleParameters_txtHour" style="width: 85px;
                                                                                        text-align: center" onchange="TimeSelector_Hour_RuleParameters_onChange('txtHour')" />
                                                                                </td>
                                                                                <td align="center">
                                                                                    :
                                                                                </td>
                                                                                <td align="center">
                                                                                    <input type="text" id="TimeSelector_Hour_RuleParameters_txtMinute" style="width: 85px;
                                                                                        text-align: center" onchange="TimeSelector_Hour_RuleParameters_onChange('txtMinute')" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td align="center">
                                                                        <ComponentArt:ToolBar ID="TlbConfirm_pgvTime_MultiPageRuleParametersTerms" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms"
                                                                                    runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                                    Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:PageView>
                                            <ComponentArt:PageView ID="pgvDate_MultiPageRuleParametersTerms" runat="server" Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDate_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDate_RuleParameters"
                                                                Text=": تاریخ"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="Container_DateCalendars_RuleParameters">
                                                            <pcal:PersianDatePickup ID="pdpDate_RuleParameters" runat="server" CssClass="PersianDatePicker"
                                                                ReadOnly="true"></pcal:PersianDatePickup>
                                                            <table id="Container_gCalDate_RuleParameters" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td onmouseup="btn_gdpDate_RuleParameters_OnMouseUp(event)">
                                                                        <ComponentArt:Calendar ID="gdpDate_RuleParameters" runat="server" ControlType="Picker"
                                                                            PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                            SelectedDate="2008-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gdpDate_RuleParameters_OnDateChange" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                    <td style="font-size: 10px;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <img id="btn_gdpDate_RuleParameters" alt="" class="calendar_button" onclick="btn_gdpDate_RuleParameters_OnClick(event)"
                                                                            onmouseup="btn_gdpDate_RuleParameters_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <ComponentArt:Calendar ID="gCalDate_RuleParameters" runat="server" AllowMonthSelection="false"
                                                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_RuleParameters"
                                                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gCalDate_RuleParameters_OnChange" />
                                                                    <Load EventHandler="gCalDate_RuleParameters_onLoad" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                        <td>
                                                            <ComponentArt:ToolBar ID="TlbConfirm_pgvDate_MultiPageRuleParametersTerms" runat="server"
                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms"
                                                                        runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                        Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms"
                                                                        TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
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
    <asp:HiddenField runat="server" ID="hfheader_tblDefineUiValidationDetails_DefineUiValidationIntroduction"
        meta:resourcekey="hfheader_tblDefineUiValidationDetails_DefineUiValidationIntroduction" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_RuleParameters" />
    <asp:HiddenField runat="server" ID="hfheader_ParameterDefineUiValidation_DefineUiValidation"
        meta:resourcekey="hfheader_ParameterDefineUiValidation_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfheader_DefineUiValidation_DefineUiValidation"
        meta:resourcekey="hfheader_DefineUiValidation_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfView_DefineUiValidation" meta:resourcekey="hfView_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfAdd_DefineUiValidation" meta:resourcekey="hfAdd_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfEdit_DefineUiValidation" meta:resourcekey="hfEdit_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfDelete_DefineUiValidation" meta:resourcekey="hfDelete_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_DefineUiValidation" meta:resourcekey="hfDeleteMessage_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_DefineUiValidation" meta:resourcekey="hfCloseMessage_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridDefineUiValidation_DefineUiValidation"
        meta:resourcekey="hfloadingPanel_GridDefineUiValidation_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridParameterDefineUiValidation_DefineUiValidation"
        meta:resourcekey="hfloadingPanel_GridParameterDefineUiValidation_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfErrorType_DefineUiValidation" meta:resourcekey="hfErrorType_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfConnectionError_DefineUiValidation" meta:resourcekey="hfConnectionError_DefineUiValidation" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogDefineUiValidation" meta:resourcekey="hfTitle_DialogDefineUiValidation" />
    </form>
</body>
</html>
