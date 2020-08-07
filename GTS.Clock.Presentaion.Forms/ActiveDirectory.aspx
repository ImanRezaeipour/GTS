<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActiveDirectory.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.ActiveDirectory" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/upload.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/rotator.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/colorPickerStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="ActiveDirectoryForm" runat="server" meta:resourcekey="ActiveDirectoryForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="font-family: Arial; font-size: small; background-color: White; background-image: url(Images/Ghadir/bg-body.jpg)"
        width="700px">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbActiveDirectory" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbActiveDirectory" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbActiveDirectory" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbActiveDirectory" runat="server" ClientSideCommand="alert('Cut');"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbActiveDirectory"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbActiveDirectory" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbActiveDirectory" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbActiveDirectory" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbActiveDirectory" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbActiveDirectory_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbActiveDirectory" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbActiveDirectory" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbActiveDirectory" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbActiveDirectory" TextImageSpacing="5"
                                        ClientSideCommand="parent.DialogActiveDirectory.Close();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemMessage_TlbActiveDirectory" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Blue message SH.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemMessage_TlbActiveDirectory" TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 70%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 33%">
                                        <asp:Label ID="lblName_ActiveDirectory" runat="server" meta:resourcekey="lblName_ActiveDirectory"
                                            Text=": نام " CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td style="width: 33%">
                                        <asp:Label ID="lblFamily_ActiveDirectory" runat="server" meta:resourcekey="lblFamily_ActiveDirectory"
                                            Text=": نام خانوادگی" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td style="width: 33%">
                                        <asp:Label ID="lblPersonnelID_ActiveDirectory" runat="server" meta:resourcekey="lblPersonnelID_ActiveDirectory"
                                            Text=": شماره پرسنلی" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="text" runat="server" style="width: 95%;" class="TextBoxes" id="txtName_ActiveDirectory" />
                                    </td>
                                    <td>
                                        <input type="text" runat="server" style="width: 95%;" class="TextBoxes" id="txtFamily_ActiveDirectory" />
                                    </td>
                                    <td>
                                        <input type="text" runat="server" style="width: 95%;" class="TextBoxes" id="txtPersonnelID_ActiveDirectory" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 30%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table style="width: 80%; background-image: url('Images/Ghadir/bg-body.jpg'); border: outset 1px black;
                                background-repeat: repeat">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        Active Directory
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:DataGrid ID="GridActiveDirectory_ActiveDirectory" runat="server" CallbackCacheSize="20000"
                                            CallbackCachingEnabled="true" CssClass="Grid" EnableViewState="true" FillContainer="false"
                                            FooterCssClass="GridFooter" Height="100%" ImagesBaseUrl="images/Grid/" LoadingPanelClientTemplateId="GridActiveDirectory_ActiveDirectory_LoadingFeedbackTemplate"
                                            LoadingPanelFadeDuration="1000" LoadingPanelFadeMaximumOpacity="60" LoadingPanelPosition="MiddleCenter"
                                            ManualPaging="true" PagePaddingEnabled="true" PagerStyle="Numbered" PagerTextCssClass="GridFooterText"
                                            PageSize="10" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="ID" DefaultSortDirection="Descending"
                                                            HeadingText="نام دامین" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDomainName_GridActiveDirectory_ActiveDirectory"
                                                            Width="85" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                            HeadingText="نام کاربری دامین" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDomainUserName_GridActiveDirectory_ActiveDirectory"
                                                            Width="115" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="GridActiveDirectory_ActiveDirectory_LoadingFeedbackTemplate">
                                                    <table bgcolor="#e0e0e0" style="width: 100; height: 50">
                                                        <tr>
                                                            <td align="center" valign="middle">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="font-size: 10px; font-family: Verdana;">
                                                                            Loading...&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img alt="" border="0" height="16" src="images/Grid/spinner.gif" width="16" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                        </ComponentArt:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="DetailsBoxHeaderStyle">
                            <div id="header_tblActiveDirectory_ActiveDirectory" runat="server" meta:resourcekey="AlignObj"
                                style="color: White; width: 100%;height: 100%" class="BoxContainerHeader">                                
                                Active Directory Details</div>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDomainName_ActiveDirectory" runat="server" meta:resourcekey="lblDomainName_ActiveDirectory"
                                            Text=": نام دامین" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbDomainName_ActiveDirectory" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            Style="width: 100%" TextBoxCssClass="comboTextBox">
                                        </ComponentArt:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDomainUserName_ActiveDirectory" runat="server" meta:resourcekey="lblDomainUserName_ActiveDirectory"
                                            Text=": نام کاربری دامین" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbDomainUserName_ActiveDirectory" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            Style="width: 100%" TextBoxCssClass="comboTextBox">
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
    </form>
</body>
</html>
