<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.Login"
    CodeFile="Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript">
        window.history.forward(1);
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/login.css" type="text/css" rel="Stylesheet" />
    <link href="css/keyboard.css" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" type="text/css" rel="Stylesheet" />
    <link href="css/ButtonStyle.css" type="text/css" rel="Stylesheet" />
    <link href="Images/Ghadir/favicon.ico" rel="Shortcut Icon" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="js/jquery.js"></script>
    <form id="LoginForm" runat="server" meta:resourcekey="LoginForm">
    <div align="center">
        <table width="100%">
            <tr>
                <td valign="middle" align="center">
                    <asp:Login ID="theLogincontrol" runat="server" DestinationPageUrl="~/MainPage.aspx"
                        meta:resourcekey="theLogincontrol" Width="40%">
                        <LayoutTemplate>
                            <table class="login_table">
                                <tr style="height: 50%">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <img alt="logo" src="Images/other/Atlas Logo.png" style="margin-left: auto; margin-right: auto;
                                            margin-top: 0px;" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr style="height: 9%">
                                    <td id="Td1" style="width: 30%" runat="server" meta:resourcekey="InverseAlignObj">
                                        <asp:Label ID="lblUserName_Login" runat="server" meta:resourcekey="lblUserName_Login"
                                            Text="UserName:"></asp:Label>
                                    </td>
                                    <td style="width: 40%">
                                        <asp:TextBox CssClass="text_box" ID="UserName" runat="server"></asp:TextBox>
                                    </td>
                                    <td runat="server" meta:resourcekey="AlignObj">
                                        <span>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                Text="*" Style="width: 99%"></asp:RequiredFieldValidator></span>
                                    </td>
                                </tr>
                                <tr style="height: 9%">
                                    <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                        <asp:Label ID="lblPassword_Login" runat="server" meta:resourcekey="lblPassword_Login"
                                            Text="Password:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="text_box" ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                    <td runat="server" meta:resourcekey="AlignObj">
                                        <span>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                Text="*"></asp:RequiredFieldValidator></span><span>
                                                    <img alt="" onclick="ShowKeyboard();" src="images/other/keyboard.png" /></span>
                                    </td>
                                </tr>
                                <tr id="SecurityCodeContainer" style="height: 9%" runat="server">
                                    <td runat="server" meta:resourcekey="InverseAlignObj">
                                        <asp:Label ID="lblSecrityCode_Login" runat="server" Text="Security Code:" meta:resourcekey="lblSecrityCode_Login"></asp:Label>
                                    </td>
                                    <td runat="server" meta:resourcekey="InverseAlignObj">
                                        <asp:TextBox CssClass="text_box" ID="SecurityCode" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="center" runat="server" meta:resourcekey="AlignObj">
                                        <img alt="" src="SecurityImageViewer.aspx" />
                                    </td>
                                </tr>
                                <tr style="height: 9%">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td id="Td4" runat="server">
                                        <table style="width: 100%; height: 34px;">
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="Login" runat="server" CommandName="Login" meta:resourcekey="Login"
                                                        OnClick="Login_Click" Text="Login" CssClass="buttonStyle" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr style="height: 24%">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td id="Td5" valign="top" runat="server" colspan="2" meta:resourcekey="AlignObj" style="color:Black">
                                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:Login>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField runat="server" meta:resourcekey="hftitle" />
    </form>
</body>
</html>
