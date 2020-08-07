<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkHeat.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.WorkHeat" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
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
    <form id="WorkHeatForm" runat="server" meta:resourcekey="WorkHeatForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; height: 400px;" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <ComponentArt:ToolBar ID="TlbWorkHeatIntroduction" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbWorkHeatIntroduction" runat="server"
                                        ClientSideCommand="tlbItemNew_TlbWorkHeatIntroduction_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbWorkHeatIntroduction" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbWorkHeatIntroduction" runat="server"
                                        ClientSideCommand="tlbItemEdit_TlbWorkHeatIntroduction_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEdit_TlbWorkHeatIntroduction" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbWorkHeatIntroduction" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbWorkHeatIntroduction_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbWorkHeatIntroduction" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbWorkHeatIntroduction" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbWorkHeatIntroduction"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbWorkHeatIntroduction_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbWorkHeatIntroduction" runat="server"
                                        ClientSideCommand="tlbItemSave_TlbWorkHeatIntroduction_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbWorkHeatIntroduction" TextImageSpacing="5"
                                        Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbWorkHeatIntroduction" runat="server"
                                        Enabled="false" ClientSideCommand="tlbItemCancel_TlbWorkHeatIntroduction_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbWorkHeatIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbWorkHeatIntroduction"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbWorkHeatIntroduction_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbWorkHeatIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbWorkHeatIntroduction" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbWorkHeatIntroduction_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbWorkHeatIntroduction" TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_WorkHeat" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="70%">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%" class="BoxStyle">
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_WorkHeat_WorkHeat" class="HeaderLabel" style="width: 50%">
                                                    Work Heats
                                                </td>
                                                <td id="loadingPanel_GridWorkHeat_WorkHeat" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridWorkHeat_WorkHeat" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridWorkHeat_WorkHeat" runat="server"
                                                                ClientSideCommand="Refresh_GridWorkHeat_WorkHeat();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridWorkHeat_WorkHeat"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridWorkHeat_WorkHeat" OnCallback="CallBack_GridWorkHeat_WorkHeat_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridWorkHeat_WorkHeat" runat="server" CssClass="Grid"
                                                    EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                                    ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                                    HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkHeatCode_GridWorkHeat" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending" AllowEditing="True"
                                                                    HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkHeatName_GridWorkHeat" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                    HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkHeatDescription_GridWorkHeat" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridWorkHeat_WorkHeat_onItemSelect" />
                                                        <Load EventHandler="GridWorkHeat_WorkHeat_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_WorkHeat" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridWorkHeat_WorkHeat_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridWorkHeat_WorkHeat_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 40%" align="center">
                            <table style="width: 80%;" id="tblWorkHeat_WorkHeatIntroduction" class="BoxStyle">
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_tblWorkHeatDetails_WorkHeatIntroduction" runat="server" class="BoxContainerHeader"
                                            meta:resourcekey="AlignObj" style="width: 100%; height: 100%">
                                            Work Heat Details</div>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblWorkHeatCode_WorkHeatIntroduction" runat="server" meta:resourcekey="lblWorkHeatCode_WorkHeatIntroduction"
                                            Text=": کد نوبت کاری" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" class="TextBoxes" id="txtWorkHeatCode_WorkHeatIntroduction"
                                            disabled="disabled" style="width: 98%" onfocus="this.select();" onclick="this.select();" />
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblWorkHeatName_WorkHeatIntroduction" runat="server" meta:resourcekey="lblWorkHeatName_WorkHeatIntroduction"
                                            Text=": نام نوبت کاری" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtWorkHeatName_WorkHeatIntroduction"
                                            disabled="disabled" onfocus="this.select();" onclick="this.select();" />
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblWorkHeatDescription_WorkHeatIntroduction" runat="server" meta:resourcekey="lblWorkHeatDescription_WorkHeatIntroduction"
                                            Text=": توضیحات" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <textarea id="txtWorkHeatDescription_WorkHeatIntroduction" cols="20" rows="2" style="width: 98%;
                                            height: 35px" disabled="disabled" class="TextBoxes" onfocus="this.select();"
                                            onclick="this.select();"></textarea>
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
    <asp:HiddenField runat="server" ID="hfheader_tblWorkHeatDetails_WorkHeatIntroduction"
        meta:resourcekey="hfheader_tblWorkHeatDetails_WorkHeatIntroduction" />
    <asp:HiddenField runat="server" ID="hfheader_WorkHeat_WorkHeat" meta:resourcekey="hfheader_WorkHeat_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfView_WorkHeat" meta:resourcekey="hfView_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfAdd_WorkHeat" meta:resourcekey="hfAdd_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfEdit_WorkHeat" meta:resourcekey="hfEdit_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfDelete_WorkHeat" meta:resourcekey="hfDelete_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_WorkHeat" meta:resourcekey="hfDeleteMessage_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_WorkHeat" meta:resourcekey="hfCloseMessage_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridWorkHeat_WorkHeat" meta:resourcekey="hfloadingPanel_GridWorkHeat_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfErrorType_WorkHeat" meta:resourcekey="hfErrorType_WorkHeat" />
    <asp:HiddenField runat="server" ID="hfConnectionError_WorkHeat" meta:resourcekey="hfConnectionError_WorkHeat" />
    </form>
</body>
</html>
