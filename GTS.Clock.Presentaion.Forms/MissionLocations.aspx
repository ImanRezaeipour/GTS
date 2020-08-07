<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MissionLocations.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.MissionLocations" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MissionLocationsForm" runat="server" meta:resourcekey="MissionLocationsForm">
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
                            <ComponentArt:ToolBar ID="TlbMissionOverallLocation" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbMissionOverallLocation" runat="server"
                                        ClientSideCommand="tlbItemNew_TlbMissionOverallLocation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbMissionOverallLocation" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbMissionOverallLocation" runat="server"
                                        ClientSideCommand="tlbItemEdit_TlbMissionOverallLocation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEdit_TlbMissionOverallLocation" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbMissionOverallLocation" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbMissionOverallLocation();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbMissionOverallLocation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMissionOverallLocation" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMissionOverallLocation"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbMissionOverallLocation_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbMissionOverallLocation" runat="server"
                                        ClientSideCommand="tlbItemSave_TlbMissionOverallLocation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        Enabled="false" ItemType="Command" TextImageSpacing="5" meta:resourcekey="tlbItemSave_TlbMissionOverallLocation" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbMissionOverallLocation" runat="server"
                                        ClientSideCommand="tlbItemCancel_TlbMissionOverallLocation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        Enabled="false" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbMissionOverallLocation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMissionOverallLocation"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbMissionOverallLocation_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMissionOverallLocation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMissionOverallLocation" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbMissionOverallLocation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbMissionOverallLocation" TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_MissionOverallLocationForm" class="ToolbarMode">
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
                                                <td id="header_MissionLocations_MissionLocationsIntroduction" class="HeaderLabel"
                                                    style="width: 50%">
                                                    Mission Locations
                                                </td>
                                                <td id="loadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction"
                                                    class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvMissionLocationsIntroduction_MissionLocationsIntroduction"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvMissionLocationsIntroduction_MissionLocationsIntroduction"
                                                                runat="server" ClientSideCommand="Refresh_trvMissionLocationsIntroduction_MissionLocationsIntroduction();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvMissionLocationsIntroduction_MissionLocationsIntroduction"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction"
                                            OnCallback="CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvMissionLocationsIntroduction_MissionLocationsIntroduction"
                                                    runat="server" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                    FillContainer="false" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="true"
                                                    EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                    ExpandImageUrl="images/TreeView/col.gif" Height="330" HoverNodeCssClass="HoverTreeNode"
                                                    ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                                    NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" Width="100%" BorderColor="Black"
                                                    meta:resourcekey="trvMissionLocationsIntroduction_MissionLocationsIntroduction">
                                                    <ClientEvents>
                                                        <NodeSelect EventHandler="trvMissionLocationsIntroduction_MissionLocationsIntroduction_onNodeSelect" />
                                                        <Load EventHandler="trvMissionLocationsIntroduction_MissionLocationsIntroduction_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_MissionLocations" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="middle" align="center">
                            <table style="width: 100%;" class="BoxStyle" id="tblMissionLocations_MissionLocationIntroduction">
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_MissionLocationsDetails_MissionLocationIntroduction" runat="server"
                                                        meta:resourcekey="AlignObj" style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                        Mission Location Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMissionLocationCode_MissionLocationIntroduction" runat="server"
                                                        meta:resourcekey="lblMissionLocationCode_MissionLocationIntroduction" Text=": کد محل ماموریت"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtMissionLocationCode_MissionLocationIntroduction"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMissionLocationName_MissionLocationIntroduction" runat="server"
                                                        meta:resourcekey="lblMissionLocationName_MissionLocationIntroduction" Text=": نام محل ماموریت"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtMissionLocationName_MissionLocationIntroduction"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
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
        runat="server" Width="300px">
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
    <asp:HiddenField runat="server" ID="hfheader_MissionLocations_MissionLocationsIntroduction"
        meta:resourcekey="hfheader_MissionLocations_MissionLocationsIntroduction" />
    <asp:HiddenField runat="server" ID="hfheader_MissionLocationsDetails_MissionLocationIntroduction"
        meta:resourcekey="hfheader_MissionLocationsDetails_MissionLocationIntroduction" />
    <asp:HiddenField runat="server" ID="hfView_MissionLocations" meta:resourcekey="hfView_MissionLocations" />
    <asp:HiddenField runat="server" ID="hfAdd_MissionLocations" meta:resourcekey="hfAdd_MissionLocations" />
    <asp:HiddenField runat="server" ID="hfEdit_MissionLocations" meta:resourcekey="hfEdit_MissionLocations" />
    <asp:HiddenField runat="server" ID="hfDelete_MissionLocations" meta:resourcekey="hfDelete_MissionLocations" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_MissionLocations" meta:resourcekey="hfDeleteMessage_MissionLocations" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MissionLocations" meta:resourcekey="hfCloseMessage_MissionLocations" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction"
        meta:resourcekey="hfloadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction" />
    <asp:HiddenField runat="server" ID="hfErrorType_MissionLocations" meta:resourcekey="hfErrorType_MissionLocations" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MissionLocations" meta:resourcekey="hfConnectionError_MissionLocations" />
    </form>
</body>
</html>
