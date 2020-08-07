<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SinglePageDataAccessLevels.aspx.cs"
    Inherits="SinglePageDataAccessLevels" %>

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
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="SinglePageDataAccessLevelsForm" runat="server" meta:resourcekey="SinglePageDataAccessLevelsForm">
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
                                    <td id="header_DataAccessLevelsSource_SinglePageDataAccessLevels" style="width: 40%"
                                        class="HeaderLabel">
                                    </td>
                                    <td id="loadingPanel_GridDataAccessLevelsSource_SinglePageDataAccessLevels" style="width: 24%">
                                        &nbsp;
                                    </td>
                                    <td runat="server" id="Container_chbSelectAll_SinglePageDataAccessLevels" style="width: 32%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbSelectAll_SinglePageDataAccessLevels" type="checkbox" class="WhiteLabel" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSelectAll_SinglePageDataAccessLevels" runat="server" Text="همه"
                                                        meta:resourcekey="lblSelectAll_SinglePageDataAccessLevels"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td runat="server" style="width: 4%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridDataAccessLevelsSource_SinglePageDataAccessLevels"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridDataAccessLevelsSource_SinglePageDataAccessLevels"
                                                    runat="server" ClientSideCommand="Refresh_GridDataAccessLevelsSource_SinglePageDataAccessLevels();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridDataAccessLevelsSource_SinglePageDataAccessLevels"
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
                            <ComponentArt:CallBack ID="CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels"
                                runat="server" OnCallback="CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridDataAccessLevelsSource_SinglePageDataAccessLevels"
                                        runat="server" AllowColumnResizing="false" AllowMultipleSelect="false" CssClass="Grid"
                                        EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="17" RunningMode="Client"
                                        ScrollBar="On" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
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
                                                        HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDataAccessLevelSourceCode_GridDataAccessLevelsSource_SinglePageDataAccessLevels" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                        HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDataAccessLevelName_GridDataAccessLevelsSource_SinglePageDataAccessLevels" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <Load EventHandler="GridDataAccessLevelsSource_SinglePageDataAccessLevels_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField ID="ErrorHiddenField_DataAccessLevelsSource" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="middle" style="width: 4%">
                <ComponentArt:ToolBar ID="TlbInterAction_SinglePageDataAccessLevels" runat="server"
                    CssClass="verticaltoolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbInterAction_SinglePageDataAccessLevels"
                            runat="server" ClientSideCommand="tlbItemAdd_TlbInterAction_SinglePageDataAccessLevels_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdd_TlbInterAction_SinglePageDataAccessLevels"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbInterAction_SinglePageDataAccessLevels"
                            runat="server" ClientSideCommand="tlbItemDelete_TlbInterAction_SinglePageDataAccessLevels_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbInterAction_SinglePageDataAccessLevels"
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
                                    <td id="header_DataAccessLevelsTarget_SinglePageDataAccessLevels" style="width: 60%"
                                        class="HeaderLabel">
                                        &nbsp;
                                    </td>
                                    <td id="loadingPanel_GridDataAccessLevelsTarget_SinglePageDataAccessLevels" style="width: 36%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 4%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridDataAccessLevelsTarget_SinglePageDataAccessLevels"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridDataAccessLevelsTarget_SinglePageDataAccessLevels"
                                                    runat="server" ClientSideCommand="Refresh_GridDataAccessLevelsTarget_SinglePageDataAccessLevels();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridDataAccessLevelsTarget_SinglePageDataAccessLevels"
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
                            <ComponentArt:CallBack ID="CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels"
                                runat="server" OnCallback="CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridDataAccessLevelsTarget_SinglePageDataAccessLevels"
                                        runat="server" AllowColumnResizing="false" AllowMultipleSelect="false" CssClass="Grid"
                                        EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="17" RunningMode="Client"
                                        ScrollBar="On" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
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
                                                        HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDataAccessLevelSourceCode_GridDataAccessLevelsTarget_SinglePageDataAccessLevels" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                        HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDataAccessLevelSourceName_GridDataAccessLevelsSource_SinglePageDataAccessLevels" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <Load EventHandler="GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onLoad" />
                                            <ItemSelect EventHandler="GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onItemSelect"/>
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField ID="ErrorHiddenField_DataAccessLevelsTarget" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
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
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridDataAccessLevelsSource_SinglePageDataAccessLevels"
        meta:resourcekey="hfloadingPanel_GridDataAccessLevelsSource_SinglePageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridDataAccessLevelsTarget_SinglePageDataAccessLevels"
        meta:resourcekey="hfloadingPanel_GridDataAccessLevelsTarget_SinglePageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfErrorType_SinglePageDataAccessLevels" meta:resourcekey="hfErrorType_SinglePageDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfConnectionError_SinglePageDataAccessLevels"
        meta:resourcekey="hfConnectionError_SinglePageDataAccessLevels" />
    </form>
</body>
</html>
