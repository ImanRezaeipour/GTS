<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EndorsementFlowState.aspx.cs"
    Inherits="EndorsementFlowState" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="~/css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="EndorsementFlowStateForm" runat="server" meta:resourcekey="EndorsementFlowStateForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td id="header_EndorsementFlowState_EndorsementFlowState" class="HeaderLabel" style="width: 65%">
                            Endorsement Flow State
                        </td>
                        <td id="loadingPanel_GridEndorsementFlowState_EndorsementFlowState" class="HeaderLabel"
                            style="width: 30%">
                        </td>
                        <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                            <ComponentArt:ToolBar ID="TlbRefresh_GridEndorsementFlowState_EndorsementFlowState"
                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridEndorsementFlowState_EndorsementFlowState"
                                        runat="server" ClientSideCommand="Refresh_GridEndorsementFlowState_EndorsementFlowState();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridEndorsementFlowState_EndorsementFlowState"
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
                <table id="Container_GridEndorsementFlowState_EndorsementFlowState"  style="width: 100%">                   
                    <tr>
                        <td>
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridEndorsementFlowState_EndorsementFlowState"
                                OnCallback="CallBack_GridEndorsementFlowState_EndorsementFlowState_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridEndorsementFlowState_EndorsementFlowState" runat="server"
                                        AllowHorizontalScrolling="false" CssClass="Grid" EnableViewState="true" ShowFooter="false"
                                        FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PageSize="10" RunningMode="Client" AllowMultipleSelect="false"
                                        AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ManagerName" DefaultSortDirection="Descending"
                                                        HeadingText="مدیر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnManager_GridEndorsementFlowState_EndorsementFlowState"
                                                        Width="100" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RequestStatus" DefaultSortDirection="Descending"
                                                        HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridEndorsementFlowState_EndorsementFlowState"
                                                        Width="50" DataCellClientTemplateId="DataCellClientTemplateId_clmnRequestStatus_GridEndorsementFlowState_EndorsementFlowState" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheDate" DefaultSortDirection="Descending"
                                                        HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDateTime_GridEndorsementFlowState_EndorsementFlowState"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                        HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridEndorsementFlowState_EndorsementFlowState"
                                                        Width="150" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestStatus_GridEndorsementFlowState_EndorsementFlowState">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" style="font-family: Verdana; font-size: 10px;" title="##SetCellTitle_GridEndorsementFlowState_EndorsementFlowState(DataItem.GetMember('RequestStatus').Value)##">
                                                            <img src="##SetClmnImage_GridEndorsementFlowState_EndorsementFlowState(DataItem.GetMember('RequestStatus').Value)##"
                                                                alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <ClientEvents>
                                            <Load EventHandler="GridEndorsementFlowState_EndorsementFlowState_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_InFlow_EndorsementFlowState" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridEndorsementFlowState_EndorsementFlowState_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridEndorsementFlowState_EndorsementFlowState_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
                <table id="Container_txtEndorsementFlowState_EndorsementFlowState" style="width: 100%;">                   
                    <tr>
                        <td>
                            <ComponentArt:CallBack runat="server" ID="CallBack_txtEndorsementFlowState_EndorsementFlowState" OnCallback="CallBack_txtEndorsementFlowState_EndorsementFlowState_onCallBack">
                                 <Content>
                                <asp:TextBox ID="txtEndorsementFlowState_EndorsementFlowState" runat="server" CssClass="TextBoxes"
                                    TextMode="MultiLine" Style="width: 100%; height: 235px" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_PendingFlow_EndorsementFlowState" />
                                </Content>
                                <ClientEvents>
                                <CallbackComplete EventHandler="CallBack_txtEndorsementFlowState_EndorsementFlowState_onCallbackComplete"/>
                                <CallbackError EventHandler="CallBack_txtEndorsementFlowState_EndorsementFlowState_onCallbackError"/>
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfTitle_DialogEndorsementFlowState" meta:resourcekey="hfTitle_DialogEndorsementFlowState" />
    <asp:HiddenField runat="server" ID="hfheader_EndorsementFlowState_EndorsementFlowState"
        meta:resourcekey="hfheader_EndorsementFlowState_EndorsementFlowState" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridEndorsementFlowState_EndorsementFlowState"
        meta:resourcekey="hfloadingPanel_GridEndorsementFlowState_EndorsementFlowState" />
    <asp:HiddenField runat="server" ID="hfRequestStates_EndorsementFlowState" />
    <asp:HiddenField runat="server" ID="hfErrorType_EndorsementFlowState" meta:resourcekey="hfErrorType_EndorsementFlowState" />
    <asp:HiddenField runat="server" ID="hfConnectionError_EndorsementFlowState" meta:resourcekey="hfConnectionError_EndorsementFlowState" />
    </form>
</body>
</html>
