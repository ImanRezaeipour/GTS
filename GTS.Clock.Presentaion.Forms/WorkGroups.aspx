<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkGroups.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.WorkGroups" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="WorkGroupsForm" runat="server" meta:resourcekey="WorkGroupsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 90%; height: 400px; font-family: Arial; font-size: small;">
        <tr>
            <td>
                &nbsp;
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbWorkGroups" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbWorkGroups" runat="server" ClientSideCommand="tlbItemNew_TlbWorkGroups_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbWorkGroups"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbWorkGroups" runat="server" ClientSideCommand="tlbItemEdit_TlbWorkGroups_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbWorkGroups"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbWorkGroups" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbWorkGroups" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemDelete_TlbWorkGroups_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCalendar_TlbWorkGroups" runat="server" ClientSideCommand="tlbItemCalendar_TlbWorkGroups_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Calendar_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCalendar_TlbWorkGroups"
                                        TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbWorkGroups" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbWorkGroups" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbWorkGroups_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbWorkGroups" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbWorkGroups" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemSave_TlbWorkGroups_onClick();" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbWorkGroups" runat="server" Enabled="false"
                                        ClientSideCommand="tlbItemCancel_TlbWorkGroups_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbWorkGroups" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbWorkGroups" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbWorkGroups_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbWorkGroups"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbWorkGroups" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ClientSideCommand="tlbItemExit_TlbWorkGroups_onClick();" ItemType="Command" meta:resourcekey="tlbItemExit_TlbWorkGroups"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_WorkGroup" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%" valign="top">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_WorkGroup_Group" class="HeaderLabel" style="width: 50%">
                                                    Work Groups
                                                </td>
                                                <td id="loadingPanel_GridWorkGroup_WorkGroup" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridGroup_Group" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridGroup_Group" runat="server"
                                                                ClientSideCommand="Refresh_GridGroup_Group();" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                                ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command"
                                                                meta:resourcekey="tlbItemRefresh_TlbRefresh_GridGroup_Group" TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridWorkGroups_WorkGroups" OnCallback="CallBack_GridWorkGroups_WorkGroups_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridWorkGroups_WorkGroups" runat="server" ShowFooter="false"
                                                    CssClass="Grid" EnableViewState="true" FillContainer="true" ImagesBaseUrl="images/Grid/"
                                                    AllowMultipleSelect="false" PagePaddingEnabled="true" PageSize="14" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" Width="100%" AllowColumnResizing="false"
                                                    ScrollBar="On" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                    ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
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
                                                                    HeadingText="کد گروه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnGroupCode_GridWorkGroups_WorkGroups" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام گروه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnGroupName_GridWorkGroups_WorkGroups" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridWorkGroups_WorkGroups_onItemSelect" />
                                                        <Load EventHandler="GridWorkGroups_WorkGroups_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_WorkGroups" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridWorkGroups_WorkGroups_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridWorkGroups_WorkGroups_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="middle" align="center">
                            <table style="width: 90%" id="tblWorkGroupDetails_WorkGroups">
                                <tr id="Tr6" runat="server" meta:resourcekey="AlignObj">
                                    <td id="Td2" runat="server">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_tblWorkGroupDetails_WorkGroups" runat="server" meta:resourcekey="AlignObj"
                                                        class="BoxContainerHeader" style="color: White; width: 100%; height: 100%">
                                                        Work Group Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblGroupCode_WorkGroups" runat="server" meta:resourcekey="lblGroupCode_WorkGroups"
                                                        Text=": کد گروه کاری" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtGroupCode_WorkGroups"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblGroupName_WorkGroups" runat="server" meta:resourcekey="lblGroupName_WorkGroups"
                                                        Text=": نام گروه کاری" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtGroupName_WorkGroups"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblYear_WorkGroups" runat="server" meta:resourcekey="lblYear_WorkGroup"
                                                        Text=": سال" CssClass="WhiteLabel">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:ComboBox ID="cmbYear_WorkGroup" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        TextBoxCssClass="comboTextBox" Width="100%" SelectedIndex="0" Enabled="false"
                                                        DropDownHeight="100">
                                                    </ComponentArt:ComboBox>
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
    <asp:HiddenField runat="server" ID="hfheader_tblWorkGroupDetails_WorkGroups" meta:resourcekey="hfheader_tblWorkGroupDetails_WorkGroups" />
    <asp:HiddenField runat="server" ID="hfheader_WorkGroup_Group" meta:resourcekey="hfheader_WorkGroup_Group" />
    <asp:HiddenField runat="server" ID="hfView_WorkGroup" meta:resourcekey="hfView_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfAdd_WorkGroup" meta:resourcekey="hfAdd_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfEdit_WorkGroup" meta:resourcekey="hfEdit_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfDelete_WorkGroup" meta:resourcekey="hfDelete_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_WorkGroup" meta:resourcekey="hfDeleteMessage_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_WorkGroup" meta:resourcekey="hfCloseMessage_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridWorkGroup_WorkGroup" meta:resourcekey="hfloadingPanel_GridWorkGroup_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfErrorType_WorkGroup" meta:resourcekey="hfErrorType_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfConnectionError_WorkGroup" meta:resourcekey="hfConnectionError_WorkGroup" />
    <asp:HiddenField runat="server" ID="hfCurrentYear_WorkGroup" />
    </form>
</body>
</html>
