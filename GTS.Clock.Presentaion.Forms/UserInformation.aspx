<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInformation.aspx.cs"
    Inherits="UserInformation" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="UserInformationForm" runat="server" meta:resourcekey="UserInformationForm">     
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="font-family: Arial; font-size: small; width:100%; height: 395px" class="BoxStyle">
        <tr>
            <td valign="top">
                <ComponentArt:CallBack runat="server" ID="CallBack_BListUserInformation" OnCallback="CallBack_BListUserInformation_onCallback">
                    <Content>
                        <asp:BulletedList runat="server" ID="BListUserInformation_UserInformation">
                        </asp:BulletedList>
                        <asp:HiddenField runat="server" ID="ErrorHiddenField_UserInformation"/>
                    </Content>
                    <ClientEvents>
                        <CallbackComplete EventHandler="CallBack_BListUserInformation_onCallbackComplete"/>
                        <CallbackError EventHandler="CallBack_BListUserInformation_onCallbackError" />
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfTitle_DialogUserInformation" meta:resourcekey="hfTitle_DialogUserInformation" />
    <asp:HiddenField runat="server" ID="hfErrorType_UserInformation" meta:resourcekey="hfErrorType_UserInformation" />
    <asp:HiddenField runat="server" ID="hfConnectionError_UserInformation" meta:resourcekey="hfConnectionError_UserInformation" />
    </form>
</body>
</html>
