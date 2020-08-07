<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ReportsForm" runat="server" meta:resourcekey="ReportsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 90%; height: 400px; font-family: Arial; font-size: small">
        <tr>
            <td valign="top">
                <table style="width: 100%; height: 400px">
                    <tr>
                        <td style="height: 10%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 60%">
                                        <ComponentArt:ToolBar ID="TlbReports" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemNewGroup_TlbReports" runat="server" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ClientSideCommand="tlbItemNewGroup_TlbReports_onClick();"
                                                    ImageHeight="16px" ImageUrl="group.png" ImageWidth="16px" ItemType="Command"
                                                    meta:resourcekey="tlbItemNewGroup_TlbReports" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNewReport_TlbReports" runat="server" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ClientSideCommand="tlbItemNewReport_TlbReports_onClick();"
                                                    ImageHeight="16px" ImageUrl="newReport.png" ImageWidth="16px" ItemType="Command"
                                                    meta:resourcekey="tlbItemNewReport_TlbReports" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbReports" runat="server" ClientSideCommand="tlbItemEdit_TlbReports_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbReports"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbReports" runat="server" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ClientSideCommand="tlbItemDelete_TlbReports_onClick();"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbReports"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemReportsParametersRegulation_TlbReports" runat="server"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png"
                                                    ClientSideCommand="tlbItemReportsParametersRegulation_TlbReports_onClick();"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReportsParametersRegulation_TlbReports"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemReportView_TlbReports" runat="server" ClientSideCommand="tlbItemReportView_TlbReports_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Report.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReportView_TlbReports"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbReports" runat="server" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemHelp_TlbReports" TextImageSpacing="5"
                                                    ClientSideCommand="tlbItemHelp_TlbReports_onClick();" />
                                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbReports" runat="server" ClientSideCommand="tlbItemSave_TlbReports_onClick();"
                                                    Enabled="false" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                                    ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbReports"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbReports" runat="server" Enabled="false"
                                                    ClientSideCommand="tlbItemCancel_TlbReports_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemCancel_TlbReports" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbReports" runat="server"
                                                    ClientSideCommand="tlbItemFormReconstruction_TlbReports_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbReports" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbReports" runat="server" ClientSideCommand="tlbItemExit_TlbReports_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbReports"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="ActionMode_Reports" class="ToolbarMode" style="width: 40%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width:60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Reports_Reports" class="HeaderLabel" style="width: 50%">
                                                    Reports
                                                </td>
                                                <td id="loadingPanel_trvReports_Reports" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvReports_Reports" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvReports_Reports" runat="server"
                                                                ClientSideCommand="Refresh_trvReports_Reports();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvReports_Reports"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvReports_Reports" OnCallback="CallBack_trvReports_Reports_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvReports_Reports" runat="server" ExpandNodeOnSelect="true"
                                                    CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                    DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                    EnableViewState="false" ExpandCollapseImageHeight="15" LoadingFeedbackText="loading......."
                                                    ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                    ForceHighlightedNodeID="true" Height="330" HoverNodeCssClass="HoverNestingTreeNode"
                                                    ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                                    NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" meta:resourcekey="trvReports_Reports"
                                                    BorderColor="Black">
                                                    <ClientEvents>
                                                        <NodeSelect EventHandler="trvReports_Reports_onNodeSelect" />
                                                        <Load EventHandler="trvReports_Reports_onLoad" />
                                                        <NodeMouseDoubleClick EventHandler="trvReports_Reports_NodeMouseDoubleClick" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField ID="ErrorHiddenField_Reports" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvReports_Reports_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvReports_Reports_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                                    </td>
                                    <td>
                            <table style="width: 90%">
                                <tr>
                                    <td>
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_ReportGroupDetails_Reports" runat="server" meta:resourcekey="AlignObj"
                                                        style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                        Report Group Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblReportGroupName_Reports" runat="server" Text=": نام گروه گزارش"
                                                        CssClass="WhiteLabel" meta:resourcekey="lblReportGroupName_Reports"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtReportGroupName_Reports" type="text" class="TextBoxes" style="width: 98%"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_ReportDetails_Reports" runat="server" class="BoxContainerHeader"
                                                        meta:resourcekey="AlignObj" style="color: White; width: 100%; height: 100%">
                                                        Report Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblReportName_Reports" runat="server" Text=": نام گزارش" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblReportName_Reports"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtReportName_Reports" type="text" class="TextBoxes" style="width: 98%"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblReportSelect_Reports" runat="server" Text=": انتخاب گزارش" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblReportSelect_Reports"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack ID="CallBack_cmbReportFiles_Reports" runat="server" OnCallback="CallBack_cmbReportFiles_Reports_onCallBack"
                                                        Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbReportFiles_Reports" runat="server" AutoComplete="true"
                                                                AutoHighlight="false" CssClass="comboBox" DataTextField="Description" DataFields="Name"
                                                                DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownPageSize="7" DropDownWidth="400"
                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbReportFiles_Reports"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox"
                                                                ExpandDirection="Up" Enabled="false" TextBoxEnabled="true">
                                                                <ClientTemplates>
                                                                    <ComponentArt:ClientTemplate ID="ItemTemplate_cmbReportFiles_Reports">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                                            <tr class="dataRow">
                                                                                <td class="dataCell" style="width: 50%">
                                                                                    ## DataItem.getProperty('Text') ##
                                                                                </td>
                                                                                <td class="dataCell" style="width: 50%">
                                                                                    ## DataItem.getProperty('Name') ##
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ComponentArt:ClientTemplate>
                                                                </ClientTemplates>
                                                                <DropDownHeader>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                                        <tr class="headingRow">
                                                                            <td id="clmnFileName_cmbReportFiles_Reports" class="headingCell" style="width: 50%;
                                                                                text-align: center">
                                                                                Report File Name
                                                                            </td>
                                                                            <td id="clmnDescription_cmbReportFiles_Reports" class="headingCell" style="width: 50%;
                                                                                text-align: center">
                                                                                Report Description
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DropDownHeader>
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbReportFiles_Reports_onExpand" />
                                                                    <Collapse EventHandler="cmbReportFiles_Reports_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_ReportFiles_Reports" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbReportFiles_Reports_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbReportFiles_Reports_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbReportFiles_Reports_onCallbackError" />
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
    <asp:HiddenField runat="server" ID="hfheader_Reports_Reports" meta:resourcekey="hfheader_Reports_Reports" />
    <asp:HiddenField runat="server" ID="hfheader_ReportDetails_Reports" meta:resourcekey="hfheader_ReportDetails_Reports" />
    <asp:HiddenField runat="server" ID="hfheader_ReportGroupDetails_Reports" meta:resourcekey="hfheader_ReportGroupDetails_Reports" />
    <asp:HiddenField runat="server" ID="hfView_Reports" meta:resourcekey="hfView_Reports" />
    <asp:HiddenField runat="server" ID="hfAdd_Reports" meta:resourcekey="hfAdd_Reports" />
    <asp:HiddenField runat="server" ID="hfEdit_Reports" meta:resourcekey="hfEdit_Reports" />
    <asp:HiddenField runat="server" ID="hfDelete_Reports" meta:resourcekey="hfDelete_Reports" />
    <asp:HiddenField runat="server" ID="hfReportDeleteMessage_Reports" meta:resourcekey="hfReportDeleteMessage_Reports" />
    <asp:HiddenField runat="server" ID="hfReportGroupDeleteMessage_Reports" meta:resourcekey="hfReportGroupDeleteMessage_Reports" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Reports" meta:resourcekey="hfCloseMessage_Reports" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvReports_Reports" meta:resourcekey="hfloadingPanel_trvReports_Reports" />
    <asp:HiddenField runat="server" ID="hfErrorType_Reports" meta:resourcekey="hfErrorType_Reports" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Reports" meta:resourcekey="hfConnectionError_Reports" />
    <asp:HiddenField runat="server" ID="hfclmnFileName_cmbReportFiles_Reports" meta:resourcekey="hfclmnFileName_cmbReportFiles_Reports" />
    <asp:HiddenField runat="server" ID="hfclmnDescription_cmbReportFiles_Reports" meta:resourcekey="hfclmnDescription_cmbReportFiles_Reports" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_Reports" meta:resourcekey="hfcmbAlarm_Reports" />
    <asp:HiddenField runat="server" ID="hfReportGroup_Reports" meta:resourcekey="hfReportGroup_Reports" />
    <asp:HiddenField runat="server" ID="hfReport_Reports" meta:resourcekey="hfReport_Reports" />
    </form>
</body>
</html>
