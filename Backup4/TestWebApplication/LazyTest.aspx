<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LazyTest.aspx.cs" Inherits="TestWebApplication.LazyTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Person Load By Barcode" OnClick="btn1_Click" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
                </td>
                <td>
                    Result:<asp:TextBox runat="server" ID="TextBoxr1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Person Load By Id" OnClick="Button2_Click" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox2"></asp:TextBox>
                </td>
                <td>
                    Result:<asp:TextBox runat="server" ID="TextBoxr2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="Person Load By Id AND Use Dependences"
                        OnClick="Button3_Click" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox3"></asp:TextBox>
                </td>
                <td>
                    Result:<asp:TextBox runat="server" ID="TextBoxr3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button4" runat="server" Text="Role Load By Id AND Use Dependences"
                        OnClick="Button4_Click" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox4"></asp:TextBox>
                </td>
                <td>
                    Result:<asp:TextBox runat="server" ID="TextBoxr4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button5" runat="server" Text="User AND Use Dependences" OnClick="Button5_Click" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox5"></asp:TextBox>
                </td>
                <td>
                    Result:<asp:TextBox runat="server" ID="TextBoxr5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button6" runat="server" Text="Insert Working Person" 
                        onclick="Button6_Click"  />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox6"></asp:TextBox>
                </td>
                <td>
                    Result:<asp:TextBox runat="server" ID="TextBoxr6"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
