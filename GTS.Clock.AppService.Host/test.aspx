<%@ Page Language="C#" AutoEventWireup="True" CodeFile="test.aspx.cs" Inherits="GTS.Clock.AppService.Host.test"
    EnableSessionState="True" Buffer="true" Debug="true" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script type="text/javascript">
    function f() {
        alert('<%= DateTime.Now.ToString() %>');
    
     }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Manual Execute</h1>
        <br />
        <table>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="prsIdTB"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Engine" OnClick="ExecuteBtn_Click"
                        Style="height: 26px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="Button2" runat="server" Text="Traffic" OnClick="Traffic_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Execute All" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Button" />
                </td>
            </tr>
             <tr>
                <td colspan="2">
                    <asp:Button ID="Button5" runat="server" Text="Execute by person" />
                </td>
            </tr>
             <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
