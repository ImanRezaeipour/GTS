<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PeriodRepeat.aspx.cs" Inherits="PeriodRepeat" %>

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
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="PeriodRepeatForm" runat="server" meta:resourcekey="PeriodRepeatForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_PeriodRepeat" style="width: 100%; font-family: Arial; font-size: small"
        class="BoxStyle">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbPeriodRepeat" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbPeriodRepeat" runat="server"
                            ClientSideCommand="tlbItemEndorsement_TlbPeriodRepeat_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbPeriodRepeat" TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPeriodRepeat" runat="server"
                            ClientSideCommand="tlbItemFormReconstruction_TlbPeriodRepeat_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPeriodRepeat"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPeriodRepeat" runat="server" ClientSideCommand="tlbItemExit_TlbPeriodRepeat_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPeriodRepeat"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblFromMonth_PeriodRepeat" runat="server" Text=": از ماه" CssClass="WhiteLabel"
                                meta:resourcekey="lblFromMonth_PeriodRepeat"></asp:Label>
                        </td>
                        <td>
                            <ComponentArt:ComboBox ID="cmbFromMonth_PeriodRepeat" runat="server" AutoComplete="true"
                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                DropDownHeight="190" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                                Width="100">
                                <ClientEvents>
                                    <Expand EventHandler="cmbFromMonth_PeriodRepeat_onExpand" />
                                    <Change EventHandler="cmbFromMonth_PeriodRepeat_onChange" />
                                </ClientEvents>
                            </ComponentArt:ComboBox>
                        </td>
                        <td style="width: 10%">
                            <asp:Label ID="lblFromDay_PeriodRepeat" runat="server" Text=": روز" CssClass="WhiteLabel"
                                meta:resourcekey="lblFromDay_PeriodRepeat"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbFromDay_PeriodRepeat" OnCallback="CallBack_cmbFromDay_PeriodRepeat_onCallBack"
                                Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbFromDay_PeriodRepeat" runat="server" AutoComplete="true"
                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropDownHeight="190" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                        HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                        SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                                        Width="50">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbFromDay_PeriodRepeat_onExpand" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_FromDay_PeriodRepeat" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_cmbFromDay_PeriodRepeat_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbFromDay_PeriodRepeat_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblToMonth_PeriodRepeat" runat="server" Text=": تا ماه" CssClass="WhiteLabel"
                                meta:resourcekey="lblToMonth_PeriodRepeat"></asp:Label>
                        </td>
                        <td>
                            <ComponentArt:ComboBox ID="cmbToMonth_PeriodRepeat" runat="server" AutoComplete="true"
                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                DropDownHeight="190" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                                Width="100">
                                <ClientEvents>
                                    <Expand EventHandler="cmbToMonth_PeriodRepeat_onExpand" />
                                    <Change EventHandler="cmbToMonth_PeriodRepeat_onChange" />
                                </ClientEvents>
                            </ComponentArt:ComboBox>
                        </td>
                        <td style="width: 10%">
                            <asp:Label ID="lblToDay_PeriodRepeat" runat="server" Text=": روز" CssClass="WhiteLabel"
                                meta:resourcekey="lblToDay_PeriodRepeat"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbToDay_PeriodRepeat" OnCallback="CallBack_cmbToDay_PeriodRepeat_onCallBack"
                                Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbToDay_PeriodRepeat" runat="server" AutoComplete="true"
                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropDownHeight="190" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                        HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                        SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                                        Width="50">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbToDay_PeriodRepeat_onExpand" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_ToDay_PeriodRepeat" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_cmbToDay_PeriodRepeat_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbToDay_PeriodRepeat_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblHolidaysRemove_PeriodRepeat" runat="server" Text=": حذف تعطیلات"
                                CssClass="WhiteLabel" meta:resourcekey="lblHolidaysRemove_PeriodRepeat"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbHolidays_PeriodRepeat" OnCallback="CallBack_cmbHolidays_PeriodRepeat_onCallBack">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbHolidays_PeriodRepeat" runat="server" AutoComplete="true"
                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DataTextField="Name" DataValueField="ID" DropImageUrl="Images/ComboBox/ddn.png"
                                        DropDownHeight="120" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                        <DropDownContent>
                                            <ComponentArt:TreeView ID="trvHolidays_PeriodRepeat" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                ExpandImageUrl="images/TreeView/col.gif" Height="98%" HoverNodeCssClass="HoverTreeNode"
                                                ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17"
                                                NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true"
                                                Width="100%" meta:resourcekey="trvHolidays_PeriodRepeat">
                                                <ClientEvents>
                                                    <NodeCheckChange EventHandler="trvHolidays_PeriodRepeat_onNodeCheckChange" />
                                                </ClientEvents>
                                            </ComponentArt:TreeView>
                                        </DropDownContent>
                                        <ClientEvents>
                                            <Expand EventHandler="cmbHolidays_PeriodRepeat_onExpand" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Holidays" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="cmbHolidays_PeriodRepeat_onBeforeCallback" />
                                    <CallbackError EventHandler="cmbHolidays_PeriodRepeat_onCallbackError" />
                                    <CallbackComplete EventHandler="cmbHolidays_PeriodRepeat_onCallbackComplete" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                        <td style="width: 5%">
                            <ComponentArt:ToolBar ID="TlbRefresh_cmbHolidays_PeriodRepeat" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbHolidays_PeriodRepeat"
                                        runat="server" ClientSideCommand="Refresh_cmbHolidays_PeriodRepeat();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbHolidays_PeriodRepeat"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogPeriodRepeat" meta:resourcekey="hfTitle_DialogPeriodRepeat" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_PeriodRepeat" meta:resourcekey="hfCloseMessage_PeriodRepeat" />
    <asp:HiddenField runat="server" ID="hfErrorType_PeriodRepeat" meta:resourcekey="hfErrorType_PeriodRepeat" />
    <asp:HiddenField runat="server" ID="hfConnectionError_PeriodRepeat" meta:resourcekey="hfConnectionError_PeriodRepeat" />
    </form>
</body>
</html>
