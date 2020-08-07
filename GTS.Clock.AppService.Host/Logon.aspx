<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="ActiveDirectory.Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script  type="text/javascript">
        function LOAD() {
            var net = new ActiveXObject("WScript.Network");
//            IWEDIT1IWCL.value = net.UserDomain + "." + net.UserName;
//            TXTCODEIWCL.value = net.UserName;
            alert(net.UserDomain + "." + net.UserName);
         }
    </script>
</head>
<body onload="LOAD();">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Domain:
                </td>
                <td>
                    <asp:TextBox ID="txtDomainName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    User:
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Password:
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="loginBtn" runat="server" Text="Login" OnClick="loginBtn_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError"  runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
