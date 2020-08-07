<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GTS.Clock.RuleDesigner.UI.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript">
        window.history.forward(1);
        //document.attachEvent("onkeydown", my_onkeydown_handler);
        function my_onkeydown_handler() {
            switch (event.keyCode) {
                case 116: // F5;
                    event.returnValue = false;
                    event.keyCode = 0;
                    window.status = "We have disabled F5";
                    break;
            }
        }
    </script>
    <title id="title"></title>
    <link href="css/login.css" type="text/css" rel="Stylesheet" />
    <link href="css/keyboard.css" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" type="text/css" rel="Stylesheet" />
    <link href="css/ButtonStyle.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="JS/API/Login_onPageLoad.js"></script>
    <script type="text/javascript" src="JS/API/Login_Operations.js"></script>
    <script type="text/javascript" src="JS/API/keyboard.js"></script>
    <form id="LoginForm" runat="server" meta:resourcekey="LoginForm">
        <div align="center">
            <table width="100%">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Login DestinationPageUrl="~/MainPage.aspx" ID="theLogincontrol" meta:resourcekey="theLogincontrol" runat="server" Width="40%">
                            <LayoutTemplate>

                                <table class="login_table">
                                    <tr style="height: 40%">
                                        <td>&nbsp;
                                        </td>
                                        <td align="center">
                                            <img alt="logo" src="Images/other/login4.png" style="margin-left: auto; margin-right: auto; margin-top: 0px;" />
                                        </td>
                                        <td>&nbsp;
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
                                        <td>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                meta:resourcekey="UserNameRequired" Style="width: 99%"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="height: 9%">
                                        <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                            <asp:Label ID="lblPassword_Login" runat="server" meta:resourcekey="lblPassword_Login"
                                                Text="Password:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="text_box" ID="Password" runat="server"
                                                TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                meta:resourcekey="PasswordRequired"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server" meta:resourcekey="AlignObj" style="height: 9%">
                                        <td></td>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="RememberMe" runat="server" meta:resourcekey="RememberMe" Text="Remember me" Visible="false" />
                                                    </td>
                                                    <td id="Td3" runat="server" meta:resourcekey="InverseAlignObj">
                                                        <img alt="" onclick="ShowKeyboard();" src="images/other/keyboard.png" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr style="height: 9%">
                                        <td>&nbsp;
                                        </td>
                                        <td id="Td4" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="Login" runat="server" CommandName="Login" meta:resourcekey="Login"
                                                            OnClick="Login_Click" Text="Login" CssClass="buttonStyle" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr style="height: 24%">
                                        <td>&nbsp;
                                        </td>
                                        <td id="Td5" valign="top" runat="server" colspan="2" meta:resourcekey="AlignObj">
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
        <asp:HiddenField ID="HiddenField1" runat="server" meta:resourcekey="hftitle" />
    </form>
</body>
</html>
