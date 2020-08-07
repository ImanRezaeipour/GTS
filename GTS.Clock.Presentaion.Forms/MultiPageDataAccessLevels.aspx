<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiPageDataAccessLevels.aspx.cs"
    Inherits="MultiPageDataAccessLevels" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MultiPageDataAccessLevelsForm" runat="server" meta:resourcekey="MultiPageDataAccessLevelsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
        <tr>
            <td style="width: 48%" class="BoxStyle">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels" runat="server"
                                                        meta:resourcekey="lblDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels"
                                                        Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels" />
                                                            </td>
                                                            <td style="width: 5%">
                                                                <ComponentArt:ToolBar ID="TlbDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels"
                                                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels"
                                                                            runat="server" ClientSideCommand="tlbItemSearch_TlbDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels"
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
                                    <td id="header_DataAccessLevelsSource_MultiPageDataAccessLevels" style="width: 40%"
                                        class="HeaderLabel">
                                    </td>
                                    <td id="loadingPanel_GridDataAccessLevelsSource_MultiPageDataAccessLevels" style="width: 24%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 32%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbSelectAll_MultiPageDataAccessLevels" type="checkbox" class="WhiteLabel" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSelectAll_MultiPageDataAccessLevels" runat="server" Text="همه"
                                                        meta:resourcekey="lblSelectAll_MultiPageDataAccessLevels"></asp:Label>
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
                            <ComponentArt:CallBack ID="CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                runat="server" OnCallback="CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                        runat="server" AllowColumnResizing="false" AllowMultipleSelect="false" CssClass="Grid"
                                        EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="12" RunningMode="Client"
                                        ScrollBar="Off" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
                                        ScrollButtonWidth="16" ScrollGripCssClass="ScrollGrip" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16"
                                        SearchTextCssClass="GridHeaderText" ShowFooter="false" Width="100%">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                        HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDataAccessLevelSourceCode_GridDataAccessLevelsSource_MultiPageDataAccessLevels" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                        HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDataAccessLevelName_GridDataAccessLevelsSource_MultiPageDataAccessLevels" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <Load EventHandler="GridDataAccessLevelsSource_MultiPageDataAccessLevels_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField ID="ErrorHiddenField_DataAccessLevelsSource" runat="server" />
                                    <asp:HiddenField runat="server" ID="hfDataAccessLevelsSourceCount_MultiPageDataAccessLevels" />
                                    <asp:HiddenField runat="server" ID="hfDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 64%;" runat="server" meta:resourcekey="AlignObj">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="footer_GridDataAccessLevelsSource_MultiPageDataAccessLevels" style="width: 36%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="middle" style="width: 4%">
                <ComponentArt:ToolBar ID="TlbInterAction_MultiPageDataAccessLevels" runat="server"
                    CssClass="verticaltoolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbInterAction_MultiPageDataAccessLevels"
                            runat="server" ClientSideCommand="tlbItemAdd_TlbInterAction_MultiPageDataAccessLevels_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdd_TlbInterAction_MultiPageDataAccessLevels"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbInterAction_MultiPageDataAccessLevels"
                            runat="server" ClientSideCommand="tlbItemDelete_TlbInterAction_MultiPageDataAccessLevels_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbInterAction_MultiPageDataAccessLevels"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
            <td class="BoxStyle">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels" runat="server"
                                                        meta:resourcekey="lblDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels"
                                                        Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels" />
                                                            </td>
                                                            <td style="width: 5%">
                                                                <ComponentArt:ToolBar ID="TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels"
                                                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels"
                                                                            runat="server" ClientSideCommand="tlbItemSearch_TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels"
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
                                    <td id="header_DataAccessLevelsTarget_MultiPageDataAccessLevels" style="width: 60%"
                                        class="HeaderLabel">
                                        &nbsp;
                                    </td>
                                    <td id="loadingPanel_GridDataAccessLevelsTarget_MultiPageDataAccessLevels" style="width: 36%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:CallBack ID="CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                runat="server" OnCallback="CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                        runat="server" AllowColumnResizing="false" AllowMultipleSelect="false" CssClass="Grid"
                                        EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="12" RunningMode="Client"
                                        ScrollBar="Off" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
                                        ScrollButtonWidth="16" ScrollGripCssClass="ScrollGrip" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16"
                                        SearchTextCssClass="GridHeaderText" ShowFooter="false" Width="100%">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                        HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDataAccessLevelSourceCode_GridDataAccessLevelsTarget_MultiPageDataAccessLevels" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                        HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDataAccessLevelSourceName_GridDataAccessLevelsSource_MultiPageDataAccessLevels" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <Load EventHandler="GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onLoad" />
                                            <ItemSelect EventHandler="GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onItemSelect"/>
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField ID="ErrorHiddenField_DataAccessLevelsTarget" runat="server" />
                                    <asp:HiddenField runat="server" ID="hfDataAccessLevelsTargetCount_MultiPageDataAccessLevels" />
                                    <asp:HiddenField runat="server" ID="hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 64%;" runat="server" meta:resourcekey="AlignObj">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="footer_GridDataAccessLevelsTarget_MultiPageDataAccessLevels" style="width: 36%">
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
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
        meta:resourcekey="hfloadingPanel_GridDataAccessLevelsSource_MultiPageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
        meta:resourcekey="hfloadingPanel_GridDataAccessLevelsTarget_MultiPageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfErrorType_MultiPageDataAccessLevels" meta:resourcekey="hfErrorType_MultiPageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MultiPageDataAccessLevels"
        meta:resourcekey="hfConnectionError_MultiPageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hffooter_GridDataAccessLevelsSource_MultiPageDataAccessLevels"
        meta:resourcekey="hffooter_GridDataAccessLevelsSource_MultiPageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hffooter_GridDataAccessLevelsTarget_MultiPageDataAccessLevels"
        meta:resourcekey="hffooter_GridDataAccessLevelsTarget_MultiPageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfDataAccessLevelsSourcePageSize_MultiPageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfDataAccessLevelsTargetPageSize_MultiPageDataAccessLevels" />
    </form>
</body>
</html>
