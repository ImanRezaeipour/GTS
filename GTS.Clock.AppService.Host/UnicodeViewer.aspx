<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnicodeViewer.aspx.cs"
    Inherits="GTS.Clock.AppService.Host.UnicodeViewer" %>

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
                    <asp:TextBox ID="tb1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn1" runat="server" OnClick="Btn1Click" Text="Show" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lbl1"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
