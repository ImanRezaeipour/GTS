<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CultureYearMonth.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.CultureYearMonth" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="CultureYearMonthForm" runat="server" meta:resourcekey="CultureYearMonthForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <div style="height: 290px;" class="BoxStyle">
        <table style="width: 50%; font-family: Arial; font-size: small">
            <tr>
                <td style="width: 45%">
                    <asp:Label ID="lblYear_CultureYearMonth" runat="server" Text=": سال" CssClass="WhiteLabel"
                        meta:resourcekey="lblYear_CultureYearMonth"></asp:Label>
                </td>
                <td style="width: 45%">
                    <asp:Label ID="lblMonth_CultureYearMonth" runat="server" Text=": ماه" CssClass="WhiteLabel"
                        meta:resourcekey="lblMonth_CultureYearMonth"></asp:Label>
                </td>
                <td style="width: 10%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <ComponentArt:ComboBox ID="cmbYear_CultureYearMonth" runat="server" AutoComplete="true"
                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                        DropDownHeight="100" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                        HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                        SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                        Width="100">
                        <ClientEvents>
                            <Change EventHandler="cmbYear_CultureYearMonth_onChange" />
                        </ClientEvents>
                    </ComponentArt:ComboBox>
                </td>
                <td>
                    <ComponentArt:ComboBox ID="cmbMonth_CultureYearMonth" runat="server" AutoComplete="true"
                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                        DropDownHeight="100" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                        HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                        SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                        Width="100">
                        <ClientEvents>
                            <Change EventHandler="cmbMonth_CultureYearMonth_onChange" />
                        </ClientEvents>
                    </ComponentArt:ComboBox>
                </td>
                <td>
                    <ComponentArt:ToolBar ID="TlbRegister_CultureYearMonth" runat="server" CssClass="toolbar"
                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemRegister_TlbRegister_CultureYearMonth" runat="server"
                                ClientSideCommand="tlbItemRegister_TlbRegister_CultureYearMonth_onClick();" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemRegister_TlbRegister_CultureYearMonth"
                                TextImageSpacing="5" Enabled="true" />
                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
        </table>
    </div>
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
    <asp:HiddenField runat="server" ID="hfCurrentYear_CultureYearMonth" />
    <asp:HiddenField runat="server" ID="hfCurrentMonth_CultureYearMonth" />
    <asp:HiddenField runat="server" ID="hfConnectionError_CultureYearMonth" meta:resourcekey="hfConnectionError_CultureYearMonth" />
    <asp:HiddenField runat="server" ID="hfErrorType_CultureYearMonth" meta:resourcekey="hfErrorType_CultureYearMonth" />
    <asp:HiddenField runat="server" ID="ReportParameterID" />
    </form>
</body>
</html>
