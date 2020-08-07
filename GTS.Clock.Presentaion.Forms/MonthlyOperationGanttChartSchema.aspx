<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyOperationGanttChartSchema.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.MonthlyOperationGanttChartSchema" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="MonthlyOperationGanttChartSchemaForm" runat="server" meta:resourcekey="MonthlyOperationGanttChartSchemaForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small;" class="BodyStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 76%">
                                        <ComponentArt:ToolBar ID="TlbMonthlyOperation" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRequestsView_TlbMonthlyOperation" runat="server"
                                                    ClientSideCommand="tlbItemRequestsView_TlbMonthlyOperation_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="registeredRequests.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRequestsView_TlbMonthlyOperation"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbMonthlyOperation" runat="server"
                                                    ClientSideCommand="tlbItemRefresh_TlbMonthlyOperation_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbMonthlyOperation" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemDetailsInformation_TlbMonthlyOperation" runat="server"
                                                    ClientSideCommand="tlbItemDetailsInformation_TlbMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="view_detailed.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDetailsInformation_TlbMonthlyOperation"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemGanttChartSettings_TlbMonthlyOperation" runat="server"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="package_settings.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemGanttChartSettings_TlbMonthlyOperation"
                                                    TextImageSpacing="5" ClientSideCommand="tlbItemGanttChartSettings_TlbMonthlyOperation_onClick();" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMonthlyOperation" runat="server"
                                                    ClientSideCommand="tlbItemFormReconstruction_TlbMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="symbolRefresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMonthlyOperation"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMonthlyOperation" runat="server" ClientSideCommand="tlbItemHelp_TlbMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMonthlyOperation"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMonthlyOperation" runat="server" ClientSideCommand="tlbItemExit_TlbMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMonthlyOperation"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="header_MonthlyOperation_MonthlyOperationGanttChartSchema" class="HeaderLabel">
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
                            <table style="width: 950px;">
                                <tr>
                                    <td style="width: 45%">
                                        <table style="width: 95%">
                                            <tr align="center">
                                                <td style="width: 18%">
                                                    <asp:Label ID="lblYear_MonthlyOperationGanttChartSchema" runat="server" Text=": سال"
                                                        meta:resourcekey="lblYear_MonthlyOperationGanttChartSchema" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 29%">
                                                    <ComponentArt:ComboBox ID="cmbYear_MonthlyOperationGanttChartSchema" runat="server"
                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                        DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        TextBoxCssClass="comboTextBox" Style="width: 98%;" DropDownHeight="280">
                                                        <ClientEvents>
                                                            <Change EventHandler="cmbYear_MonthlyOperationGanttChartSchema_onChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                </td>
                                                <td style="width: 18%;">
                                                    <asp:Label ID="lblMonth_MonthlyOperationGanttChartSchema" runat="server" Text=": ماه"
                                                        meta:resourcekey="lblMonth_MonthlyOperationGanttChartSchema" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 29%;">
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbMonth_MonthlyOperationGanttChartSchema"
                                                        OnCallback="CallBack_cmbMonth_MonthlyOperationGanttChartSchema_onCallBack" Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbMonth_MonthlyOperationGanttChartSchema" runat="server"
                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" Style="width: 98%;" DropDownHeight="280">
                                                                <ClientEvents>
                                                                    <Change EventHandler="cmbMonth_MonthlyOperationGanttChartSchema_onChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="hfCurrentMonth_MonthlyOperationGanttChartSchema" />
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Months_MonthlyOperationGanttChartSchema" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <CallbackComplete EventHandler="CallBack_cmbMonth_MonthlyOperationGanttChartSchema_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbMonth_MonthlyOperationGanttChartSchema_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td>
                                                    <ComponentArt:ToolBar ID="TlbView_MonthlyOperationGanttChartSchema" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_MonthlyOperationGanttChartSchema"
                                                                runat="server" ClientSideCommand="tlbItemView_TlbView_MonthlyOperationGanttChartSchema_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbView_MonthlyOperationGanttChartSchema"
                                                                TextImageSpacing="5" Enabled="true" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 10%" align="center">
                                        <asp:Label ID="lblFromDate_MonthlyOperationGanttChartSchema" runat="server" Text=": از تاریخ"
                                            meta:resourcekey="lblFromDate_MonthlyOperationGanttChartSchema" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <input id="txtFromDate_MonthlyOperationGanttChartSchema" type="text" class="TextBoxes"
                                            readonly="readonly" style="width: 95%; text-align: center" />
                                    </td>
                                    <td style="width: 10%" align="center">
                                        <asp:Label ID="lblToDate_MonthlyOperationGanttChartSchema" runat="server" Text=": تا تاریخ"
                                            meta:resourcekey="lblToDate_MonthlyOperationGanttChartSchema" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <input id="txtToDate_MonthlyOperationGanttChartSchema" type="text" class="TextBoxes"
                                            readonly="readonly" style="width: 95%; text-align: center" />
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
                <ComponentArt:CallBack runat="server" ID="CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema"
                    OnCallback="CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onCallBack">
                    <Content>
                        <DCWC:Chart ID="GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema" runat="server"
                            BackColor="#F3DFC1" Width="980px" Height="490px" BorderLineStyle="Solid" Palette="Dundas"
                            BackGradientType="TopBottom" BorderLineWidth="2" BorderLineColor="181, 64, 1"
                            AutoSize="true" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend LegendStyle="Table" AutoFitText="False" Docking="Top" Name="Default"
                                    DockToChartArea="Default" BackColor="Transparent" Font="Tahoma, 7.25pt" Alignment="Near"
                                    DockInsideChartArea="false" FontColor="Gray">
                                </DCWC:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                            <Series>
                                <DCWC:Series YValuesPerPoint="2" Name="TaskDefault" ChartType="Gantt" CustomAttributes="PointWidth=1"
                                    BorderColor="180, 26, 59, 105" Color="White" ShowInLegend="false">
                                    <Points>
                                        <DCWC:DataPoint XValue="0" YValues="0" />
                                    </Points>
                                </DCWC:Series>
                            </Series>
                            <ChartAreas>
                                <DCWC:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderStyle="Solid" BackGradientEndColor="White"
                                    BackColor="OldLace" ShadowColor="Transparent">
                                    <Position Y="100" X="100" Height="90" Width="95" />
                                    <AxisX LineColor="64, 64, 64, 64" Interval="1">
                                        <LabelStyle Font="Tahoma, 8.25pt, style=Bold" ShowEndLabels="false" FontColor="Highlight">
                                        </LabelStyle>
                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                    </AxisX>
                                    <AxisY LineColor="64, 64, 64, 64" StartFromZero="true" LabelsAutoFitStyle="None"
                                        Reverse="true" Interval="1" Minimum="0" Maximum="24">
                                        <LabelStyle Font="Arial, 8.25pt, style=Bold" FontColor="Highlight"></LabelStyle>
                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                    </AxisY>
                                    <AxisY2 LineColor="64, 64, 64, 64" StartFromZero="true" LabelsAutoFitStyle="None"
                                        Reverse="true" Interval="1" Enabled="True" Minimum="0" Maximum="24">
                                        <LabelStyle Font="Arial, 8.25pt, style=Bold" FontColor="Highlight"></LabelStyle>
                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                    </AxisY2>
                                </DCWC:ChartArea>
                            </ChartAreas>
                        </DCWC:Chart>
                        <asp:HiddenField runat="server" ID="ErrorHiddenField_MonthlyOperationGanttChartSchema" />
                        <asp:HiddenField runat="server" ID="hfTaskFeatures_MonthlyOperationGanttChartSchema" />
                    </Content>
                    <ClientEvents>
                        <CallbackComplete EventHandler="CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onCallBackComplete" />
                        <CallbackError EventHandler="CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onCallbackError" />
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogGanttChartSettings"
        runat="server" Width="200px">
        <Content>
            <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double;
                font-size: small; border-left: black 1px double; border-bottom: gray 1px double;
                width: 100%;" meta:resourcekey="tblGanttChartSettings_DialogGanttChartSettings"
                class="BodyStyle">
                <tr>
                    <td>
                        <ComponentArt:ToolBar ID="TlbGanttChartSettings" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbGanttChartSettings" runat="server" DropDownImageHeight="16px"
                                    ClientSideCommand="tlbItemSave_TlbGanttChartSettings_onClick();" DropDownImageWidth="16px"
                                    ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbGanttChartSettings"
                                    TextImageSpacing="5" Enabled="true" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbGanttChartSettings" runat="server" DropDownImageHeight="16px"
                                    ClientSideCommand="tlbItemExit_TlbGanttChartSettings_onClick();" DropDownImageWidth="16px"
                                    ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbGanttChartSettings"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table style="width: 100%;" class="BoxStyle">
                            <tr>
                                <td id="header_GridSettings_MonthlyOperationGanttChartSchema" style="color: White;
                                    font-weight: bold; font-family: Arial; width: 100%">
                                    Gantt Chart Settings
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server" meta:resourcekey="InverseAlignObj">
                                <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj">
                                    <table style="width: 60%">
                                        <tr align="center">
                                            <td style="width: 30%">
                                                <asp:Label runat="server" ID="lblSelectAll_GridSettings_MonthlyOperationGanttChartSchema"
                                                    meta:resourcekey="lblSelectAll_GridSettings_MonthlyOperationGanttChartSchema"
                                                    CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                            <td style="width: 10%">
                                                <input runat="server" id="chbSelectAll_GridSettings_MonthlyOperationGanttChartSchema"
                                                    type="checkbox" onclick="chbSelectAll_GridSettings_MonthlyOperationGanttChartSchema_onClick();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <ComponentArt:CallBack runat="server" ID="CallBack_GridSettings_MonthlyOperationGanttChartSchema"
                                        OnCallback="CallBack_GridSettings_MonthlyOperationGanttChartSchema_onCallBack">
                                        <Content>
                                            <ComponentArt:DataGrid ID="GridSettings_MonthlyOperationGanttChartSchema" runat="server"
                                                AllowMultipleSelect="false" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                                Height="495" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerStyle="Numbered"
                                                ShowFooter="false" PagerTextCssClass="GridFooterText" PageSize="17" RunningMode="Client"
                                                SearchTextCssClass="GridHeaderText" TabIndex="0" Width="180" AllowColumnResizing="false"
                                                ScrollBar="Auto" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
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
                                                            <ComponentArt:GridColumn Align="Center" DataField="ConceptCaption" DefaultSortDirection="Descending"
                                                                HeadingText="مفهوم" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnConcept_GridSettings_MonthlyOperationGanttChartSchema" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="ViewState" DefaultSortDirection="Descending"
                                                                HeadingText="نمایش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnView_GridSettings_MonthlyOperationGanttChartSchema"
                                                                ColumnType="CheckBox" AllowEditing="True" />
                                                            <ComponentArt:GridColumn DataField="ConceptTitle" Visible="false" />
                                                        </Columns>
                                                    </ComponentArt:GridLevel>
                                                </Levels>
                                            </ComponentArt:DataGrid>
                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema" />
                                            <asp:HiddenField runat="server" ID="hfCurrentSettingID_GridSettings_MonthlyOperationGanttChartSchema" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_GridSettings_MonthlyOperationGanttChartSchema_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_GridSettings_MonthlyOperationGanttChartSchema_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogUserInformationheader" FooterClientTemplateId="DialogUserInformationfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogUserInformation"
        runat="server" PreloadContentUrl="false" ContentUrl="UserInformation.aspx" IFrameCssClass="UserInformation_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogUserInformationheader">
                <table id="tbl_DialogUserInformationheader" style="width: 603px" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogUserInformation.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogUserInformation_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogUserInformation" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogUserInformation" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogUserInformation_IFrame').src = 'WhitePage.aspx'; DialogUserInformation.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogUserInformation_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogUserInformationfooter">
                <table id="tbl_DialogUserInformationfooter" style="width: 603px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogUserInformation_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogUserInformation_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogUserInformation_onShow" />
            <OnClose EventHandler="DialogUserInformation_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="300px">
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
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogHourlyRequestOnAbsence"
        HeaderClientTemplateId="DialogHourlyRequestOnAbsenceheader" FooterClientTemplateId="DialogHourlyRequestOnAbsencefooter"
        runat="server" PreloadContentUrl="false" ContentUrl="HourlyRequestOnAbsence.aspx"
        IFrameCssClass="HourlyRequestOnAbsence_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogHourlyRequestOnAbsenceheader">
                <table id="tbl_DialogHourlyRequestOnAbsence" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogHourlyRequestOnAbsence.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogHourlyRequestOnAbsence_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogHourlyRequestOnAbsence" valign="bottom" style="color: White;
                                        font-size: 13px; font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogHourlyRequestOnAbsence" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogHourlyRequestOnAbsence_IFrame').src = 'WhitePage.aspx'; DialogHourlyRequestOnAbsence.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogHourlyRequestOnAbsence_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogHourlyRequestOnAbsencefooter">
                <table id="tbl_DialogHourlyRequestOnAbsencefooter" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogHourlyRequestOnAbsence_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogHourlyRequestOnAbsence_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogHourlyRequestOnAbsence_onShow" />
            <OnClose EventHandler="DialogHourlyRequestOnAbsence_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogDailyRequestOnAbsence"
        HeaderClientTemplateId="DialogDailyRequestOnAbsenceheader" FooterClientTemplateId="DialogDailyRequestOnAbsencefooter"
        runat="server" PreloadContentUrl="false" ContentUrl="DailyRequestOnAbsence.aspx"
        IFrameCssClass="DailyRequestOnAbsence_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogDailyRequestOnAbsenceheader">
                <table id="tbl_DialogDailyRequestOnAbsenceheader" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogDailyRequestOnAbsence.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogDailyRequestOnAbsence_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogDailyRequestOnAbsence" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogDailyRequestOnAbsence" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogDailyRequestOnAbsence_IFrame').src = 'WhitePage.aspx'; DialogDailyRequestOnAbsence.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogDailyRequestOnAbsence_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogDailyRequestOnAbsencefooter">
                <table id="tbl_DialogDailyRequestOnAbsencefooter" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogDailyRequestOnAbsence_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogDailyRequestOnAbsence_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogDailyRequestOnAbsence_onShow" />
            <OnClose EventHandler="DialogDailyRequestOnAbsence_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRequestOnUnallowableOverTime"
        HeaderClientTemplateId="DialogRequestOnUnallowableOverTimeheader" FooterClientTemplateId="DialogRequestOnUnallowableOverTimefooter"
        runat="server" PreloadContentUrl="false" ContentUrl="RequestOnUnallowableOverTime.aspx"
        IFrameCssClass="RequestOnUnallowableOverTime_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogRequestOnUnallowableOverTimeheader">
                <table id="tbl_DialogRequestOnUnallowableOverTimeheader" style="width: 653px;" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogRequestOnUnallowableOverTime.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestOnUnallowableOverTime_topLeftImage" style="display: block;"
                                src="Images/Dialog/top_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogRequestOnUnallowableOverTime" valign="bottom" style="color: White;
                                        font-size: 13px; font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogRequestOnUnallowableOverTime" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogRequestOnUnallowableOverTime_IFrame').src = 'WhitePage.aspx'; DialogRequestOnUnallowableOverTime.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogRequestOnUnallowableOverTime_topRightImage" style="display: block;"
                                src="Images/Dialog/top_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogRequestOnUnallowableOverTimefooter">
                <table id="tbl_DialogRequestOnUnallowableOverTimefooter" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestOnUnallowableOverTime_downLeftImage" style="display: block;"
                                src="Images/Dialog/down_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogRequestOnUnallowableOverTime_downRightImage" style="display: block;"
                                src="Images/Dialog/down_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogRequestOnUnallowableOverTime_onShow" />
            <OnClose EventHandler="DialogRequestOnUnallowableOverTime_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfheader_MonthlyOperation_MonthlyOperationGanttChartSchema"
        meta:resourcekey="hfheader_MonthlyOperation_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="hfErrorType_MonthlyOperationGanttChartSchema"
        meta:resourcekey="hfErrorType_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MonthlyOperationGanttChartSchema"
        meta:resourcekey="hfConnectionError_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="hfCurrentYear_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="hfRetErrorType_MonthlyOperationGanttChartSchema"
        meta:resourcekey="hfRetErrorType_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MonthlyOperationGanttChartSchema"
        meta:resourcekey="hfCloseMessage_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="hfCurrentUser_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="ErrorHiddenField_CalculationDateRange" meta:resourcekey="ErrorHiddenField_CalculationDateRange" />
    <asp:HiddenField runat="server" ID="hfMonthlyOperation_MonthlyOperationGanttChartSchema"
        meta:resourcekey="hfMonthlyOperation_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="hfheader_GridSettings_MonthlyOperationGanttChartSchema"
        meta:resourcekey="hfheader_GridSettings_MonthlyOperationGanttChartSchema" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogMonthlyOperationGanttChartSchema"
        meta:resourcekey="hfTitle_DialogMonthlyOperationGanttChartSchema" />
    </form>
</body>
</html>
