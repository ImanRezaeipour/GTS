<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendPrivateMessage.aspx.cs"
    Inherits="SendPrivateMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/menuStyle.css" runat="server" type="text/css" rel="stylesheet" />
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="SendPrivateMessageForm" runat="server" meta:resourcekey="SendPrivateMessageForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <div>
        <table style="width: 100%; font-family: Arial; font-size: small" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbSendPrivateMessage" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemSend_TlbSendPrivateMessage" runat="server" ClientSideCommand="tlbItemsend_TlbSendPrivateMessage_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSend_TlbSendPrivateMessage"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbSendPrivateMessage" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemHelp_TlbSendPrivateMessage_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbSendPrivateMessage"
                                            TextImageSpacing="5" Visible="true" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbSendPrivateMessage" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbSendPrivateMessage_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbSendPrivateMessage"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbSendPrivateMessage" runat="server" ClientSideCommand="tlbItemExit_TlbSendPrivateMessage_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbSendPrivateMessage"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 40%">
                    <table style="width: 100%">
                        <tr>
                            <td id="Container_AdvancedBox_SendPrivateMessage">
                                <table id="tblAdvancedBox_SendPrivateMessage" style="width: 100%;">
                                    <tr>
                                        <td style="width: 48%">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td id="header_OrganizationPersonnelBox_SendPrivateMessage" class="HeaderLabel" style="width: 55%">
                                                                    Organization Personnel
                                                                </td>
                                                                <td id="loadingPanel_trvOrganizationPersonnel_SendPrivateMessage" class="HeaderLabel"
                                                                    style="width: 40%">
                                                                </td>
                                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvOrganizationPersonnel_SendPrivateMessage"
                                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvOrganizationPersonnel_SendPrivateMessage"
                                                                                runat="server" ClientSideCommand="Refresh_trvOrganizationPersonnel_SendPrivateMessage();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvOrganizationPersonnel_SendPrivateMessage"
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
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvOrganizationPersonnel_SendPrivateMessage"
                                                            OnCallback="CallBack_trvOrganizationPersonnel_SendPrivateMessage_Callback">
                                                            <Content>
                                                                <ComponentArt:TreeView ID="trvOrganizationPersonnel_SendPrivateMessage" runat="server"
                                                                    CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                                    DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                                    ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                                    Height="280" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                                    LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                                    NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" BorderColor="Black"
                                                                    meta:resourcekey="trvOrganizationPersonnel_SendPrivateMessage">
                                                                    <ClientEvents>
                                                                        <Load EventHandler="trvOrganizationPersonnel_SendPrivateMessage_onLoad" />
                                                                        <CallbackComplete EventHandler="trvOrganizationPersonnel_SendPrivateMessage_onCallbackComplete" />
                                                                        <NodeBeforeExpand EventHandler="trvOrganizationPersonnel_SendPrivateMessage_onNodeBeforeExpand" />
                                                                        <ContextMenu EventHandler="trvOrganizationPersonnel_SendPrivateMessage_onContextMenu" />
                                                                    </ClientEvents>
                                                                </ComponentArt:TreeView>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_OrganizationPersonnel_SendPrivateMessage" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <CallbackComplete EventHandler="CallBack_trvOrganizationPersonnel_SendPrivateMessage_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_trvOrganizationPersonnel_SendPrivateMessage_onCallbackError" />
                                                            </ClientEvents>
                                                        </ComponentArt:CallBack>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 4%" align="center" valign="middle">
                                            <ComponentArt:ToolBar ID="TlbInterAction_SendPrivateMessage" runat="server" CssClass="verticaltoolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                Orientation="Vertical" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item"
                                                DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px"
                                                DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemSend_TlbInterAction_SendPrivateMessage" runat="server"
                                                        ClientSideCommand="tlbItemAdd_TlbInterAction_SendPrivateMessage_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px" ItemType="Command"
                                                        meta:resourcekey="tlbItemAdd_TlbInterAction_SendPrivateMessage" TextImageSpacing="5"
                                                        Enabled="true" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbInterAction_SendPrivateMessage" runat="server"
                                                        ClientSideCommand="tlbItemDelete_TlbInterAction_SendPrivateMessage_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbInterAction_SendPrivateMessage"
                                                        TextImageSpacing="5" Enabled="true" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td>
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td id="header_UnderManagementPersonnelBox_SendPrivateMessage" class="HeaderLabel"
                                                                    style="width: 45%">
                                                                    Send To :
                                                                </td>
                                                                <td id="loadingPanel_GridUnderManagementPersonnel_SendPrivateMessage" class="HeaderLabel"
                                                                    style="width: 50%">
                                                                </td>
                                                                <td id="Td5" runat="server" style="width: 5%; height: 16px;" meta:resourcekey="InverseAlignObj">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridUnderManagementPersonnel_SendPrivateMessage"
                                                            OnCallback="CallBack_GridUnderManagementPersonnel_SendPrivateMessage_Callback">
                                                            <Content>
                                                                <ComponentArt:DataGrid ID="GridUnderManagementPersonnel_SendPrivateMessage" runat="server"
                                                                    CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                                                    ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                                                    PageSize="12" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                                                    ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                                    Height="330" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
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
                                                                                <ComponentArt:GridColumn Align="Center" DataField="Contains" DefaultSortDirection="Descending"
                                                                                    ColumnType="CheckBox" HeadingText="دسترسی" HeadingTextCssClass="HeadingText"
                                                                                    AllowEditing="False" Width="56" meta:resourcekey="clmnAccess_GridUnderManagementPersonnel_SendPrivateMessage"
                                                                                    Visible="false" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="Type" DefaultSortDirection="Descending"
                                                                                    DataCellClientTemplateId="DataCellClientTemplateId_clmnType_GridUnderManagementPersonnel_SendPrivateMessage"
                                                                                    Width="70" HeadingText="نوع" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnType_GridUnderManagementPersonnel_SendPrivateMessage" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                                    Width="150" HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridUnderManagementPersonnel_SendPrivateMessage" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="ContainInnerChilds" DefaultSortDirection="Descending"
                                                                                    ColumnType="CheckBox" Width="136" HeadingText="زیربخش" HeadingTextCssClass="HeadingText"
                                                                                    AllowEditing="True" meta:resourcekey="clmnSubDepartment_GridUnderManagementPersonnel_SendPrivateMessage" />
                                                                                <ComponentArt:GridColumn DataField="KeyID" Visible="false" />
                                                                            </Columns>
                                                                        </ComponentArt:GridLevel>
                                                                    </Levels>
                                                                    <ClientTemplates>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnType_GridUnderManagementPersonnel_SendPrivateMessage">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td align="center" style="font-family: Verdana; font-size: 10px;">
                                                                                        <img src="##SetImage_clmnType_GridUnderManagementPersonnel_SendPrivateMessage(DataItem.GetMember('Type').Value)##"
                                                                                            alt="" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                    </ClientTemplates>
                                                                    <ClientEvents>
                                                                        <Load EventHandler="GridUnderManagementPersonnel_SendPrivateMessage_onLoad" />
                                                                        <ContextMenu EventHandler="GridUnderManagementPersonnel_SendPrivateMessage_onContextMenu" />
                                                                        <ItemBeforeCheckChange EventHandler="GridUnderManagementPersonnel_SendPrivateMessage_onItemBeforeCheckChange" />
                                                                        <ItemCheckChange EventHandler="GridUnderManagementPersonnel_SendPrivateMessage_onItemCheckChange" />
                                                                    </ClientEvents>
                                                                </ComponentArt:DataGrid>
                                                                <asp:HiddenField runat="server" ID="hfUnderManagementPersonnelList_SendPrivateMessage"
                                                                    Value="" />
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_SendPrivateMessage_SendPrivateMessage" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <CallbackComplete EventHandler="CallBack_GridUnderManagementPersonnel_SendPrivateMessage_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_GridUnderManagementPersonnel_SendPrivateMessage_onCallbackError" />
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
                        <tr>
                            <td id="tdFromPersonSendPrivateMessage_SendPrivateMessageIntroduction" meta:resourcekey="AlignObj"
                                align="center" style="font-family: tahoma; font-size: 9pt; font-weight: bold;">
                                <asp:Label ID="lblInformationSenderPerson" runat="server" Text="" meta:resourcekey="lblInformationSenderPerson"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 98%;" id="tblSendPrivateMessage_SendPrivateMessageIntroduction"
                                    class="BoxStyle" align="center">
                                    <tr id="Tr8" runat="server" meta:resourcekey="AlignObj">
                                        <td class="DetailsBoxHeaderStyle">
                                            <div id="header_tblSendPrivateMessageDetails_SendPrivateMessageIntroduction" runat="server"
                                                class="BoxContainerHeader" meta:resourcekey="AlignObj" style="width: 100%; height: 100%">
                                                SendPrivateMessage Details</div>
                                        </td>
                                    </tr>
                                    <tr id="Tr11" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <asp:Label ID="lblSendPrivateMessageSubject_SendPrivateMessageIntroduction" runat="server"
                                                meta:resourcekey="lblSendPrivateMessageSubject_SendPrivateMessageIntroduction"
                                                Text=":عنوان" Style="width: 98%;" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr12" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSendPrivateMessageSubject_SendPrivateMessageIntroduction"
                                                onfocus="this.select();" onclick="this.select();" />
                                        </td>
                                    </tr>
                                    <tr id="Tr13" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <asp:Label ID="lblSendPrivateMessageMessage_SendPrivateMessageIntroduction" runat="server"
                                                meta:resourcekey="lblSendPrivateMessageMessage_SendPrivateMessageIntroduction"
                                                Text=": متن پیام" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr14" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <textarea id="txtSendPrivateMessageMessage_SendPrivateMessageIntroduction" cols="20"
                                                rows="6" style="width: 98%; height: 80px;" class="TextBoxes" onfocus="this.select();"
                                                onclick="this.select();" name="S1"></textarea>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
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
    <asp:HiddenField runat="server" ID="hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction"
        meta:resourcekey="hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction" />
    <asp:HiddenField runat="server" ID="hfSendTo_tblPrivateMessageDetails_PrivateMessageIntroduction"
        meta:resourcekey="hfSendTo_tblPrivateMessageDetails_PrivateMessageIntroduction" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogSendPrivateMessage" meta:resourcekey="hfTitle_DialogSendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfheader_OrganizationPersonnelBox_SendPrivateMessage"
        meta:resourcekey="hfheader_OrganizationPersonnelBox_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfheader_SendPrivateMessageBox_SendPrivateMessage"
        meta:resourcekey="hfheader_UnderManagementPersonnelBox_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfheader_OrganizationPosts_SendPrivateMessage"
        meta:resourcekey="hfheader_OrganizationPosts_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_SendPrivateMessage" meta:resourcekey="hfCloseMessage_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvOrganizationPosts_SendPrivateMessage"
        meta:resourcekey="hfloadingPanel_trvOrganizationPosts_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvOrganizationPersonnel_SendPrivateMessage"
        meta:resourcekey="hfloadingPanel_trvOrganizationPersonnel_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_SendPrivateMessage" meta:resourcekey="hfcmbAlarm_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridUnderManagementPersonnel_SendPrivateMessage"
        meta:resourcekey="hfloadingPanel_GridSendPrivateMessage_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfAdd_SendPrivateMessage" meta:resourcekey="hfAdd_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfEdit_SendPrivateMessage" meta:resourcekey="hfEdit_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfUndermanagementTypesList_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfErrorType_SendPrivateMessage" meta:resourcekey="hfErrorType_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfConnectionError_SendPrivateMessage" meta:resourcekey="hfConnectionError_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfReplyPersonID_SendPrivateMessage" />
    <asp:HiddenField runat="server" ID="hfReplyMessageID_SendPrivateMessage" />
    <br />
    </form>
</body>
</html>
