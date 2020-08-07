<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemMessage.aspx.cs" Inherits="SystemMessage" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="SystemMessageForm" runat="server" meta:resourcekey="SystemMessageForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%;" class="BoxStyle">
            <tr>
                <td>
                    <asp:Label ID="lblSubject_SystemMessage" runat="server" Text=": عنوان" CssClass="WhiteLabel" meta:resourcekey="lblSubject_SystemMessage"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <input runat="server" id="txtSubject_SystemMessage" type="text" style="width:97%" class="WhiteLabel"/></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage_SystemMessage" runat="server" Text=": پیغام" CssClass="WhiteLabel" meta:resourcekey="lblMessage_SystemMessage"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <textarea runat="server" id="txtMessage_SystemMessage" cols="20" class="TextBoxes" rows="4" style="width: 97%; height: 220px;"></textarea></td>
            </tr>
            <tr>
                <td align="center">
                    <ComponentArt:ToolBar ID="TlbSystemMessage" runat="server" CssClass="toolbar"
                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" OnItemCommand="TlbSystemMessage_ItemCheckChanged"
                        UseFadeEffect="false" AutoPostBackOnSelect="true">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemSend_TlbSystemMessage" runat="server" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemSend_TlbSystemMessage"
                                TextImageSpacing="5" AutoPostBackOnSelect="True"/>
                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="hfTitle_DialogSystemMessage" meta:resourcekey="hfTitle_DialogSystemMessage"/>
        <asp:HiddenField runat="server" ID="hfMessage_SystemMessage"/>
    </form>
</body>
</html>
