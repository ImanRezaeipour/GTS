<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Physicians.aspx.cs" Inherits="Physicians" %>

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
    <form id="PhysiciansForm" runat="server" meta:resourcekey="PhysiciansForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td>
                            &nbsp;
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbPhysicians" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemNew_TlbPhysicians" runat="server" ClientSideCommand="tlbItemNew_TlbPhysicians_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbPhysicians"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbPhysicians" runat="server" ClientSideCommand="tlbItemEdit_TlbPhysicians_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbPhysicians"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbPhysicians" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemDelete_TlbPhysicians_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                                    meta:resourcekey="tlbItemDelete_TlbPhysicians" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPhysicians" runat="server" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPhysicians" TextImageSpacing="5"
                                                    ClientSideCommand="tlbItemHelp_TlbPhysicians_onClick();" />
                                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPhysicians" runat="server" ClientSideCommand="tlbItemSave_TlbPhysicians_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                    ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPhysicians"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbPhysicians" runat="server" ClientSideCommand="tlbItemCancel_TlbPhysicians_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                    ImageUrl="cancel_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbPhysicians"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPhysicians" runat="server"
                                                    ClientSideCommand="tlbItemFormReconstruction_TlbPhysicians_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPhysicians"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPhysicians" runat="server" ClientSideCommand="tlbItemExit_TlbPhysicians_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPhysicians"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="ActionMode_Physicians" class="ToolbarMode">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td valign="top" style="width: 60%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td id="header_Physicians_Physicians" class="HeaderLabel" style="width: 50%">
                                                                Physicians
                                                            </td>
                                                            <td id="loadingPanel_GridPhysicians_Physicians" class="HeaderLabel" style="width: 45%">
                                                            </td>
                                                            <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                <ComponentArt:ToolBar ID="TlbRefresh_GridPhysicians_Physicians" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridPhysicians_Physicians"
                                                                            runat="server" ClientSideCommand="Refresh_GridPhysicians_Physicians();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridPhysicians_Physicians"
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
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_GridPhysicians_Physicians" OnCallback="CallBack_GridPhysicians_Physicians_onCallBack">
                                                        <Content>
                                                            <ComponentArt:DataGrid ID="GridPhysicians_Physicians" runat="server" CssClass="Grid"
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
                                                                            <ComponentArt:GridColumn DataField="FirstName" Visible="false" />
                                                                            <ComponentArt:GridColumn DataField="LastName" Visible="false" />
                                                                            <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                                HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnNameAndFamily_GridPhysicians_Physicians" />
                                                                            <ComponentArt:GridColumn Align="Center" DataField="Takhasos" DefaultSortDirection="Descending"
                                                                                HeadingText="تخصص" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnProficiency_GridPhysicians_Physicians" />
                                                                            <ComponentArt:GridColumn Align="Center" DataField="Nezampezaeshki" DefaultSortDirection="Descending"
                                                                                HeadingText="نظام پزشکی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMedicalAssociation_GridPhysicians_Physicians" />
                                                                            <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                                HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridPhysicians_Physicians" />
                                                                        </Columns>
                                                                    </ComponentArt:GridLevel>
                                                                </Levels>
                                                                <ClientEvents>
                                                                    <ItemSelect EventHandler="GridPhysicians_Physicians_onItemSelect" />
                                                                    <Load EventHandler="GridPhysicians_Physicians_onLoad" />
                                                                </ClientEvents>
                                                            </ComponentArt:DataGrid>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Physicians" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <CallbackComplete EventHandler="CallBack_GridPhysicians_Physicians_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_GridPhysicians_Physicians_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="middle">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_tblPhysicians_Physicians" runat="server" style="color: White; width: 100%;
                                                        height: 100%" class="BoxContainerHeader">
                                                        Physician Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblName_Physicians" runat="server" CssClass="WhiteLabel" Text=": نام"
                                                        meta:resourcekey="lblName_Physicians"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtName_Physicians" type="text" style="width: 97%" class="TextBoxes" disabled="disabled"
                                                        onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFamily_Physicians" runat="server" CssClass="WhiteLabel" Text=": نام خانوادگی"
                                                        meta:resourcekey="lblFamily_Physicians"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtFamily_Physicians" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblProficiency_Physicians" runat="server" CssClass="WhiteLabel" Text=": تخصص"
                                                        meta:resourcekey="lblProficiency_Physicians"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtProficiency_Physicians" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMedicalAssociation_Physicians" runat="server" CssClass="WhiteLabel"
                                                        Text=": نظام پزشکی" meta:resourcekey="lblMedicalAssociation_Physicians"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtMedicalAssociation_Physicians" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescription_Physicians" runat="server" CssClass="WhiteLabel" Text=": توضیحات"
                                                        meta:resourcekey="lblDescription_Physicians"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <textarea id="txtDescription_Physicians" cols="20" name="S1" rows="7" style="width: 97%;
                                                        height: 35px" class="TextBoxes" disabled="disabled" onclick="this.select();"
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
    <asp:HiddenField runat="server" ID="hfheader_Physicians_Physicians" meta:resourcekey="hfheader_Physicians_Physicians" />
    <asp:HiddenField runat="server" ID="hfheader_tblPhysicians_Physicians" meta:resourcekey="hfheader_tblPhysicians_Physicians" />
    <asp:HiddenField runat="server" ID="hfView_Physicians" meta:resourcekey="hfView_Physicians" />
    <asp:HiddenField runat="server" ID="hfAdd_Physicians" meta:resourcekey="hfAdd_Physicians" />
    <asp:HiddenField runat="server" ID="hfEdit_Physicians" meta:resourcekey="hfEdit_Physicians" />
    <asp:HiddenField runat="server" ID="hfDelete_Physicians" meta:resourcekey="hfDelete_Physicians" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Physicians" meta:resourcekey="hfDeleteMessage_Physicians" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Physicians" meta:resourcekey="hfCloseMessage_Physicians" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridPhysicians_Physicians" meta:resourcekey="hfloadingPanel_GridPhysicians_Physicians" />
    <asp:HiddenField runat="server" ID="hfErrorType_Physicians" meta:resourcekey="hfErrorType_Physicians" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Physicians" meta:resourcekey="hfConnectionError_Physicians" />
    </form>
</body>
</html>
