<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrivateMessage.aspx.cs" Inherits="PrivateMessage" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="PrivateMessageForm" runat="server" meta:resourcekey="PrivateMessageForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; height: 400px; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <ComponentArt:ToolBar ID="TlbPrivateMessageIntroduction" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSend_TlbPrivateMessageIntroduction" runat="server"
                                        ClientSideCommand="tlbItemSend_TlbPrivateMessage_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSend_TlbPrivateMessageIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbPrivateMessageIntroduction" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbPrivateMessage_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbPrivateMessageIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSystemMessage_TlbPrivateMessageIntroduction" runat="server"
                                        ClientSideCommand="tlbItemSystemMessage_TlbPrivateMessageIntroduction_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Blue message SH.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSystemMessage_TlbPrivateMessageIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPrivateMessageIntroduction" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPrivateMessageIntroduction"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbPrivateMessageIntroduction_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPrivateMessageIntroduction"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbPrivateMessageIntroduction_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPrivateMessageIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TTlbPrivateMessageIntroduction" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbPrivateMessage_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbPrivateMessageIntroduction"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_PrivateMessage" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="70%">
                <div class="TabStripContainer">
                    <ComponentArt:TabStrip ID="TabStripPrivateMessageMenus" runat="server" DefaultGroupTabSpacing="1"
                        DefaultItemLookId="DefaultTabLook" ScrollingEnabled="true" ImagesBaseUrl="images/TabStrip"
                        MultiPageId="MultiPagePrivateMessageMenus" ScrollLeftLookId="ScrollItem" ScrollRightLookId="ScrollItem"
                        Width="100%" DefaultSelectedItemLookId="SelectedTabLook">
                        <ItemLooks>
                            <ComponentArt:ItemLook LookId="DefaultTabLook" CssClass="DefaultTab" HoverCssClass="DefaultTabHover"
                                LabelPaddingLeft="15" LabelPaddingRight="15" LabelPaddingTop="4" LabelPaddingBottom="4"
                                LeftIconUrl="tab_left_icon.gif" RightIconUrl="tab_right_icon.gif" LeftIconWidth="13"
                                LeftIconHeight="25" RightIconWidth="13" RightIconHeight="25" meta:resourcekey="DefaultTabLook" />
                            <ComponentArt:ItemLook LookId="SelectedTabLook" CssClass="SelectedTab" LabelPaddingLeft="15"
                                LabelPaddingRight="15" LabelPaddingTop="4" LabelPaddingBottom="4" LeftIconUrl="selected_tab_left_icon.gif"
                                RightIconUrl="selected_tab_right_icon.gif" LeftIconWidth="13" LeftIconHeight="25"
                                RightIconWidth="13" RightIconHeight="25" meta:resourcekey="SelectedTabLook" />
                            <ComponentArt:ItemLook LookId="ScrollItem" CssClass="ScrollItem" HoverCssClass="ScrollItemHover"
                                LabelPaddingLeft="5" LabelPaddingRight="5" LabelPaddingTop="0" LabelPaddingBottom="0" />
                        </ItemLooks>
                        <Tabs>
                            <ComponentArt:TabStripTab ID="tbReceive_TabStripMenus" Text="پیام های دریافتی" meta:resourcekey="tbReceive_TabStripMenus_PrivateMesssage"
                                PageViewId="pgvPrivateMessageReceive">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab ID="tbSend_TabStripMenus" Text="پیام های ارسالی" meta:resourcekey="tbSend_TabStripMenus_PrivateMesssage"
                                PageViewId="pgvPrivateMessageSend">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                        <ClientEvents>
                            <TabSelect EventHandler="TabStripPrivateMessageMenus_onTabSelect" />
                        </ClientEvents>
                    </ComponentArt:TabStrip>
                </div>
                <ComponentArt:MultiPage ID="MultiPagePrivateMessageMenus" runat="server" CssClass="MultiPage"
                    SelectedIndex="0" Width="100%">
                    <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPrivateMessageReceive">
                        <div style="width: 100%; margin: 0 auto;">
                            <table style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td valign="top" style="width: 60%">
                                                    <table style="width: 100%;" class="BoxStyle">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td id="header_PrivateMessageReceive_PrivateMessage" class="HeaderLabel" style="width: 40%">
                                                                            <asp:Label ID="lblheader_PrivateMessageReceive_PrivateMessage" meta:resourcekey="lblheader_PrivateMessageReceive_PrivateMessage"
                                                                                runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td id="loadingPanel_GridPrivateMessageReceive_PrivateMessage" class="HeaderLabel"
                                                                            style="width: 30%">
                                                                        </td>
                                                                        <td style="width: 30%">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 5%">
                                                                                        <input type="checkbox" id="chbSelectAllPrivateMessageReceive_PrivateMessage" onclick="selectAllItem_GridPrivateMessageReceive_PrivateMessage();"
                                                                                            meta:resourcekey="chbSelectAllPrivateMessageReceive_PrivateMessage" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblSelectAllPrivateMessageReceive_PrivateMessage" meta:resourcekey="lblSelectAllPrivateMessage_PrivateMessage"
                                                                                            runat="server" Text="انتخاب همه"></asp:Label>
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
                                                                <ComponentArt:CallBack runat="server" ID="CallBack_GridPrivateMessageReceive_PrivateMessage"
                                                                    OnCallback="CallBack_GridPrivateMessageReceive_PrivateMessage_onCallBack">
                                                                    <Content>
                                                                        <ComponentArt:DataGrid ID="GridPrivateMessageReceive_PrivateMessage" runat="server"
                                                                            CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                                                            PageSize="10" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowHorizontalScrolling="true"
                                                                            AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" Style="width: 100%">
                                                                            <Levels>
                                                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                                    SortImageWidth="9">
                                                                                    <Columns>
                                                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                                        <ComponentArt:GridColumn DataField="FromPersonID" Visible="false" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="Select" DefaultSortDirection="Descending"
                                                                                            AllowEditing="True" HeadingText=" " HeadingTextCssClass="HeadingText" ColumnType="CheckBox" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="Status" DefaultSortDirection="Descending"
                                                                                            HeadingText="وضعیت" AllowEditing="True" ColumnType="CheckBox" HeadingTextCssClass="HeadingText"
                                                                                            meta:resourcekey="clmnPrivateMessageReceiveStatus_GridPrivateMessage" DataCellClientTemplateId="DataCellClientTemplateId_clmnStatus_GridPrivateMessageReceive_PrivateMessage" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="FromPerson.Name" DefaultSortDirection="Descending"
                                                                                            HeadingText="ارسال کننده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPrivateMessageReceiveFromPerson_GridPrivateMessage" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="Subject" DefaultSortDirection="Descending"
                                                                                            HeadingText="عنوان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPrivateMessageReceiveSubject_GridPrivateMessage" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="TheDate" DefaultSortDirection="Descending"
                                                                                            HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPrivateMessageReceiveDate_GridPrivateMessage" />
                                                                                        <ComponentArt:GridColumn DataField="Message" Visible="false" />
                                                                                    </Columns>
                                                                                </ComponentArt:GridLevel>
                                                                            </Levels>
                                                                            <ClientTemplates>
                                                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnStatus_GridPrivateMessageReceive_PrivateMessage">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" style="font-family: Verdana; font-size: 10px;">
                                                                                                <img src="##SetImage_clmnPrivateMessageStatus_GridPrivateMessage(DataItem.GetMember('Status').Value)##"
                                                                                                    alt="" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ComponentArt:ClientTemplate>
                                                                            </ClientTemplates>
                                                                            <ClientEvents>
                                                                                <ItemSelect EventHandler="GridPrivateMessageReceive_PrivateMessage_onItemSelect" />
                                                                                <Load EventHandler="GridPrivateMessageReceive_PrivateMessage_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:DataGrid>
                                                                        <asp:HiddenField ID="errorHiddenFieldReceive_PrivateMessage" runat="server" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <CallbackComplete EventHandler="CallBack_GridPrivateMessageReceive_PrivateMessage_onCallbackComplete" />
                                                                        <CallbackError EventHandler="CallBack_GridPrivateMessageReceive_PrivateMessage_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 75%;">
                                                                            <ComponentArt:ToolBar ID="TlbPaging_GridPrivateMessageReceive_PrivateMessage" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                Style="direction: ltr" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridPrivateMessageReceive_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridPrivateMessageReceive_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridPrivateMessageReceive_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridPrivateMessageReceives_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridPrivateMessageReceive_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                        <td id="footer_GridPrivateMessageReceive_PrivateMessage" class="WhiteLabel" style="width: 25%">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td id="tdSelectAllPrivateMessageReceive_PrivateMessage" meta:resourcekey="tdSelectAllPrivateMessage_PrivateMessage">
                                                    <table style="width: 100%;" id="tblPrivateMessageReceive_PrivateMessageIntroduction"
                                                        class="BoxStyle">
                                                        <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                                            <td class="DetailsBoxHeaderStyle">
                                                                <div id="header_tblPrivateMessageReceiveDetails_PrivateMessageIntroduction" runat="server"
                                                                    class="BoxContainerHeader" meta:resourcekey="AlignObj" style="width: 100%; height: 100%">
                                                                    PrivateMessageReceive Details</div>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr2" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <asp:Label ID="lblPrivateMessageReceiveFrom_PrivateMessageIntroduction" runat="server"
                                                                    meta:resourcekey="lblPrivateMessageReceiveFrom_PrivateMessageIntroduction" Text=":فرستنده"
                                                                    CssClass="WhiteLabel" Style="width: 98%;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr3" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <input type="text" runat="server" class="TextBoxes" id="txtPrivateMessageReceiveFrom_PrivateMessageIntroduction"
                                                                    disabled="disabled" style="width: 50%;" onfocus="this.select();" onclick="this.select();" />
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr4" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <asp:Label ID="lblPrivateMessageReceiveSubject_PrivateMessageIntroduction" runat="server"
                                                                    meta:resourcekey="lblPrivateMessageReceiveSubject_PrivateMessageIntroduction"
                                                                    Text=":عنوان" Style="width: 98%;" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr5" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <input type="text" runat="server" style="width: 100%;" class="TextBoxes" id="txtPrivateMessageReceiveSubject_PrivateMessageIntroduction"
                                                                                disabled="disabled" onfocus="this.select();" onclick="this.select();" />
                                                                        </td>
                                                                        <td style="width: 50%" align="left">
                                                                            <ComponentArt:ToolBar ID="TlbReplyMessage_PrivateMessageIntroduction" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemReplyMessage_PrivateMessageIntroduction" runat="server"
                                                                                        ClientSideCommand="tlbItemReplyMessage_PrivateMessageIntroduction_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReplyMessage_PrivateMessageIntroduction"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr6" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <asp:Label ID="lblPrivateMessageReceiveMessage_PrivateMessageIntroduction" runat="server"
                                                                    meta:resourcekey="lblPrivateMessageReceiveMessage_PrivateMessageIntroduction"
                                                                    Text=": متن پیام" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr7" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <textarea id="txtPrivateMessageReceiveMessage_PrivateMessageIntroduction" cols="20"
                                                                    rows="6" style="width: 98%; height: 80px;" disabled="disabled" class="TextBoxes"
                                                                    onfocus="this.select();" onclick="this.select();" name="S1"></textarea>
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
                    </ComponentArt:PageView>
                    <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPrivateMessageSend">
                        <div style="width: 100%; margin: 0 auto;">
                            <table style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td valign="top" style="width: 60%">
                                                    <table style="width: 100%;" class="BoxStyle">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td id="header_PrivateMessageSend_PrivateMessage" class="HeaderLabel" style="width: 40%">
                                                                            <asp:Label ID="lblheader_PrivateMessageSend_PrivateMessage" meta:resourcekey="lblheader_PrivateMessageSend_PrivateMessage"
                                                                                runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td id="loadingPanel_GridPrivateMessageSend_PrivateMessage" class="HeaderLabel" style="width: 30%">
                                                                        </td>
                                                                        <td style="width: 30%">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 5%;">
                                                                                        <input type="checkbox" id="chbSelectAllPrivateMessageSend_PrivateMessage" onclick="selectAllItem_GridPrivateMessageSend_PrivateMessage();"
                                                                                            meta:resourcekey="chbSelectAllPrivateMessageSend_PrivateMessage" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblSelectAllPrivateMessageSend_PrivateMessage" meta:resourcekey="lblSelectAllPrivateMessage_PrivateMessage"
                                                                                            runat="server" Text="انتخاب همه"></asp:Label>
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
                                                                <ComponentArt:CallBack runat="server" ID="CallBack_GridPrivateMessageSend_PrivateMessage"
                                                                    OnCallback="CallBack_GridPrivateMessageSend_PrivateMessage_onCallBack">
                                                                    <Content>
                                                                        <ComponentArt:DataGrid ID="GridPrivateMessageSend_PrivateMessage" runat="server"
                                                                            CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                                                            PageSize="10" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowHorizontalScrolling="true"
                                                                            AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" Style="width: 100%">
                                                                            <Levels>
                                                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                                    SortImageWidth="9">
                                                                                    <Columns>
                                                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                                        <ComponentArt:GridColumn DataField="ToPersonID" Visible="false" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="Select" DefaultSortDirection="Descending"
                                                                                            AllowEditing="True" ColumnType="CheckBox" HeadingText=" " HeadingTextCssClass="HeadingText"
                                                                                            Width="20" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="Status" DefaultSortDirection="Descending"
                                                                                            HeadingText="وضعیت" AllowEditing="True" ColumnType="CheckBox" HeadingTextCssClass="HeadingText"
                                                                                            meta:resourcekey="clmnPrivateMessageSendStatus_GridPrivateMessage" Width="40"
                                                                                            DataCellClientTemplateId="DataCellClientTemplateId_clmnStatus_GridPrivateMessageSend_PrivateMessage" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="ToPerson.Name" DefaultSortDirection="Descending"
                                                                                            HeadingText="دریافت کننده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPrivateMessageSendToPerson_GridPrivateMessage"
                                                                                            Width="70" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="Subject" DefaultSortDirection="Descending"
                                                                                            HeadingText="عنوان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPrivateMessageSendSubject_GridPrivateMessage"
                                                                                            Width="120" />
                                                                                        <ComponentArt:GridColumn Align="Center" DataField="TheDate" DefaultSortDirection="Descending"
                                                                                            HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPrivateMessageSendDate_GridPrivateMessage"
                                                                                            Width="70" />
                                                                                        <ComponentArt:GridColumn DataField="Message" Visible="false" />
                                                                                    </Columns>
                                                                                </ComponentArt:GridLevel>
                                                                            </Levels>
                                                                            <ClientTemplates>
                                                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnStatus_GridPrivateMessageSend_PrivateMessage">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" style="font-family: Verdana; font-size: 10px;">
                                                                                                <img src="##SetImage_clmnPrivateMessageStatus_GridPrivateMessage(DataItem.GetMember('Status').Value)##"
                                                                                                    alt="" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ComponentArt:ClientTemplate>
                                                                            </ClientTemplates>
                                                                            <ClientEvents>
                                                                                <ItemSelect EventHandler="GridPrivateMessageSend_PrivateMessage_onItemSelect" />
                                                                                <Load EventHandler="GridPrivateMessageSend_PrivateMessage_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:DataGrid>
                                                                        <asp:HiddenField ID="errorHiddenFieldSend_PrivateMessage" runat="server" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <CallbackComplete EventHandler="CallBack_GridPrivateMessageSend_PrivateMessage_onCallbackComplete" />
                                                                        <CallbackError EventHandler="CallBack_GridPrivateMessageSend_PrivateMessage_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td id="Td2" runat="server" meta:resourcekey="AlignObj" style="width: 75%;">
                                                                            <ComponentArt:ToolBar ID="TlbPaging_GridPrivateMessageSend_PrivateMessage" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                Style="direction: ltr" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridPrivateMessageSend_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridPrivateMessageSend_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridPrivateMessageSend_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridPrivateMessageSend_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridPrivateMessageSend_PrivateMessage"
                                                                                        runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                        ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridPrivateMessage_PrivateMessage"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                        <td id="footer_GridPrivateMessageSend_PrivateMessage" class="WhiteLabel" style="width: 25%">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td id="tdSelectAllPrivateMessageSend_PrivateMessage" meta:resourcekey="tdSelectAllPrivateMessage_PrivateMessage">
                                                    <table style="width: 100%;" id="tblPrivateMessageSend_PrivateMessageIntroduction"
                                                        class="BoxStyle">
                                                        <tr id="Tr8" runat="server" meta:resourcekey="AlignObj">
                                                            <td class="DetailsBoxHeaderStyle">
                                                                <div id="header_tblPrivateMessageSendDetails_PrivateMessageIntroduction" runat="server"
                                                                    class="BoxContainerHeader" meta:resourcekey="AlignObj" style="width: 100%; height: 100%">
                                                                    PrivateMessageSend Details</div>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr9" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <asp:Label ID="lblPrivateMessageSendTo_PrivateMessageIntroduction" runat="server"
                                                                    meta:resourcekey="lblPrivateMessageSendTo_PrivateMessageIntroduction" Text=":فرستنده"
                                                                    CssClass="WhiteLabel" Style="width: 98%;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr10" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <input type="text" runat="server" class="TextBoxes" id="txtPrivateMessageSendTo_PrivateMessageIntroduction"
                                                                    disabled="disabled" style="width: 50%;" onfocus="this.select();" onclick="this.select();" />
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr11" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <asp:Label ID="lblPrivateMessageSendSubject_PrivateMessageIntroduction" runat="server"
                                                                    meta:resourcekey="lblPrivateMessageSendSubject_PrivateMessageIntroduction" Text=":عنوان"
                                                                    Style="width: 98%;" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr12" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <input type="text" runat="server" style="width: 50%;" class="TextBoxes" id="txtPrivateMessageSendSubject_PrivateMessageIntroduction"
                                                                    disabled="disabled" onfocus="this.select();" onclick="this.select();" />
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr13" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <asp:Label ID="lblPrivateMessageSendMessage_PrivateMessageIntroduction" runat="server"
                                                                    meta:resourcekey="lblPrivateMessageSendMessage_PrivateMessageIntroduction" Text=": متن پیام"
                                                                    CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tr14" runat="server" meta:resourcekey="AlignObj">
                                                            <td>
                                                                <textarea id="txtPrivateMessageSendMessage_PrivateMessageIntroduction" cols="20"
                                                                    rows="6" style="width: 98%; height: 80px;" disabled="disabled" class="TextBoxes"
                                                                    onfocus="this.select();" onclick="this.select();" name="S1"></textarea>
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
                    </ComponentArt:PageView>
                </ComponentArt:MultiPage>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogSystemMessageheader" FooterClientTemplateId="DialogSystemMessagefooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogSystemMessage"
            runat="server" PreloadContentUrl="false" ContentUrl="SystemMessage.aspx" IFrameCssClass="SystemMessage_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogSystemMessageheader">
                    <table id="tbl_DialogSystemMessageheader" style="width: 603px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogSystemMessage.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogSystemMessage_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogSystemMessage" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogSystemMessage" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogSystemMessage_IFrame').src = 'WhitePage.aspx'; DialogSystemMessage.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogSystemMessage_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogSystemMessagefooter">
                    <table id="tbl_DialogSystemMessagefooter" style="width: 603px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogSystemMessage_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogSystemMessage_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogSystemMessage_onShow" />
                <OnClose EventHandler="DialogSystemMessage_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
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
    <asp:HiddenField runat="server" ID="hfPrivateMessageReceivePageSize_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfPrivateMessageSendPageSize_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction"
        meta:resourcekey="hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction" />
    <asp:HiddenField runat="server" ID="hffooter_GridPrivateMessageReceive_PrivateMessage"
        meta:resourcekey="hffooter_GridPrivateMessageReceive_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hffooter_GridPrivateMessageSend_PrivateMessage"
        meta:resourcekey="hffooter_GridPrivateMessageSend_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfheader_PrivateMessage_PrivateMessage" meta:resourcekey="hfheader_PrivateMessage_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfView_PrivateMessage" meta:resourcekey="hfView_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfAdd_PrivateMessage" meta:resourcekey="hfAdd_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfEdit_PrivateMessage" meta:resourcekey="hfEdit_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfDelete_PrivateMessage" meta:resourcekey="hfDelete_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_PrivateMessage" meta:resourcekey="hfDeleteMessage_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_PrivateMessage" meta:resourcekey="hfCloseMessage_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridPrivateMessage_PrivateMessage"
        meta:resourcekey="hfloadingPanel_GridPrivateMessage_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfErrorType_PrivateMessage" meta:resourcekey="hfErrorType_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfConnectionError_PrivateMessage" meta:resourcekey="hfConnectionError_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfPrivateMessageReceivePageCount_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfPrivateMessageSendPageCount_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfPrivateMessageReceiveCount_PrivateMessage" />
    <asp:HiddenField runat="server" ID="hfPrivateMessageSendCount_PrivateMessage" />
    </form>
</body>
</html>
