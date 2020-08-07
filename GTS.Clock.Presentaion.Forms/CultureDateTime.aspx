<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CultureDateTime.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.CultureDateTime" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="~/css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="CultureDateTimeForm" runat="server" meta:resourcekey="CultureDateTimeForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div style="height: 290px;" class="BoxStyle">
            <table style="width: 50%; font-family: Arial; font-size: small">
                <tr>
                    <td style="width: 50%">
                        <asp:Label ID="lblYear_CultureDateTime" runat="server" Text=": سال" CssClass="WhiteLabel"
                            meta:resourcekey="lblYear_CultureDateTime"></asp:Label>
                    </td>
                    <td style="width: 50%">
                        <asp:Label ID="lblMonth_CultureDateTime" runat="server" Text=": ماه" CssClass="WhiteLabel"
                            meta:resourcekey="lblMonth_CultureDateTime"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <ComponentArt:ComboBox ID="cmbYear_CultureDateTime" runat="server" AutoComplete="true"
                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                            DropDownHeight="100" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                            SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                            Width="100">
                            <ClientEvents>
                                <Change EventHandler="cmbYear_CultureDateTime_onChange" />
                            </ClientEvents>
                        </ComponentArt:ComboBox>
                    </td>
                    <td>
                        <ComponentArt:ComboBox ID="cmbMonth_CultureDateTime" runat="server" AutoComplete="true"
                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                            DropDownHeight="100" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                            SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                            Width="100">
                            <ClientEvents>
                                <Change EventHandler="cmbMonth_CultureDateTime_onChange" />
                            </ClientEvents>
                        </ComponentArt:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTime_CultureDateTime" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTime_CultureDateTime"
                                        Text=": زمان"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 70%">
                                                <table dir="ltr" style="width: 100%">
                                                    <tr>
                                                        <td align="center" style="width: 48%">
                                                            <input id="TimeSelector_Hour_CultureDateTime_txtHour" onchange="TimeSelector_Hour_CultureDateTime_onChange('txtHour')"
                                                                style="width: 70%; text-align: center" type="text" />
                                                        </td>
                                                        <td align="center">:
                                                        </td>
                                                        <td align="center" style="width: 48%">
                                                            <input id="TimeSelector_Hour_CultureDateTime_txtMinute" onchange="TimeSelector_Hour_CultureDateTime_onChange('txtMinute')"
                                                                style="width: 70%; text-align: center" type="text" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <ComponentArt:ToolBar ID="TlbRegister_CultureDateTime" runat="server" CssClass="toolbar"
                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemRegister_TlbRegister_CultureDateTime" runat="server"
                                                            ClientSideCommand="tlbItemRegister_TlbRegister_CultureDateTime_onClick();" DropDownImageHeight="16px"
                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                                            ItemType="Command" meta:resourcekey="tlbItemRegister_TlbRegister_CultureDateTime"
                                                            TextImageSpacing="5" Enabled="true" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
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
        <asp:HiddenField runat="server" ID="hfCurrentYear_CultureDateTime" />
        <asp:HiddenField runat="server" ID="hfCurrentMonth_CultureDateTime" />
        <asp:HiddenField runat="server" ID="hfConnectionError_CultureDateTime" meta:resourcekey="hfConnectionError_CultureDateTime" />
        <asp:HiddenField runat="server" ID="hfErrorType_CultureDateTime" meta:resourcekey="hfErrorType_CultureDateTime" />
        <asp:HiddenField runat="server" ID="ReportParameterID" />
    </form>
</body>
</html>
