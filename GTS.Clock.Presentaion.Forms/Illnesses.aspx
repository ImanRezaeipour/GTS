<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Illnesses.aspx.cs" Inherits="Illnesses" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="IllnessesForm" runat="server" meta:resourcekey="IllnessesForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 90%; height: 400px; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbIllness" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbIllness" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemNew_TlbIllness_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbIllness"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbIllness" runat="server" ClientSideCommand="tlbItemEdit_TlbIllness_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbIllness"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbIllness" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbIllness_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbIllness" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbIllness" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidthhiftint="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbIllness" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbIllness_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbIllness" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbIllness_onClick();" DropDownImageWidth="16px"
                                        Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbIllness" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbIllness" runat="server" Enabled="false"
                                        ClientSideCommand="tlbItemCancel_TlbIllness_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbIllness" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbIllness" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbIllness_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbIllness" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbIllness" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbIllness" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemExit_TlbIllness_onClick();" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_Illness" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Illnesses_Illness" class="HeaderLabel" style="width: 50%">
                                                    Illnesses
                                                </td>
                                                <td id="loadingPanel_GridIllness_Illness" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridIllness_Illness" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridIllness_Illness" runat="server"
                                                                ClientSideCommand="Refresh_GridIllness_Illness();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridIllness_Illness"
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
                                        <ComponentArt:CallBack ID="CallBack_GridIllness_Illness" runat="server" OnCallback="CallBack_GridIllness_Illness_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridIllness_Illness" runat="server" AllowColumnResizing="false"
                                                    AllowMultipleSelect="false" CssClass="Grid" EnableViewState="true" FillContainer="true"
                                                    FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                    PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client" ScrollBar="On"
                                                    ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17" ScrollButtonWidth="16"
                                                    ScrollGripCssClass="ScrollGrip" ScrollImagesFolderUrl="images/Grid/scroller/"
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
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام بیماری" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnIllnessName_GridIllness"
                                                                    Width="150" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                    HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridIllness"
                                                                    Width="200" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridIllness_Illness_onItemSelect" />
                                                        <Load EventHandler="GridIllness_Illness_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_Illness" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridIllness_Illness_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridIllness_Illness_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="middle" align="center">
                            <table style="width: 100%;" class="BoxStyle" id="tblIllness_Illness">
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_tblIllness_Illness" runat="server" meta:resourcekey="AlignObj" style="color: White;
                                                        width: 100%; height: 100%" class="BoxContainerHeader">
                                                        Illness Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblIllnessName_Illness" runat="server" meta:resourcekey="lblIllnessName_Illness"
                                                        Text=": نام بیماری" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtIllnessName_Illness"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescription_Illness" runat="server" meta:resourcekey="lblDescription_Illness"
                                                        Text=": توضیحات" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <textarea type="text" runat="server" style="width: 98%; height: 35px" rows="7" cols="20"
                                                        class="TextBoxes" id="txtDescription_Illness" disabled="disabled" onclick="this.select();"
                                                        onfocus="this.select();"></textarea>
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
    <asp:HiddenField runat="server" ID="hfheader_Illnesses_Illness" meta:resourcekey="hfheader_Illnesses_Illness" />
    <asp:HiddenField runat="server" ID="hfheader_tblIllness_Illness" meta:resourcekey="hfheader_tblIllness_Illness" />
    <asp:HiddenField runat="server" ID="hfView_Illness" meta:resourcekey="hfView_Illness" />
    <asp:HiddenField runat="server" ID="hfAdd_Illness" meta:resourcekey="hfAdd_Illness" />
    <asp:HiddenField runat="server" ID="hfEdit_Illness" meta:resourcekey="hfEdit_Illness" />
    <asp:HiddenField runat="server" ID="hfDelete_Illness" meta:resourcekey="hfDelete_Illness" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Illness" meta:resourcekey="hfDeleteMessage_Illness" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Illness" meta:resourcekey="hfCloseMessage_Illness" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridIllness_Illness" meta:resourcekey="hfloadingPanel_GridIllness_Illness" />
    <asp:HiddenField runat="server" ID="hfErrorType_Illness" meta:resourcekey="hfErrorType_Illness" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Illness" meta:resourcekey="hfConnectionError_Illness" />
    </form>
</body>
</html>
